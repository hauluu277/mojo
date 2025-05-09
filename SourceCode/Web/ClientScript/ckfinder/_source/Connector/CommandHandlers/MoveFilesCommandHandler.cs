/*
 * CKFinder
 * ========
 * http://cksource.com/ckfinder
 * Copyright (C) 2007-2015, CKSource - Frederico Knabben. All rights reserved.
 *
 * The software, this file and its contents are subject to the CKFinder
 * License. Please read the license.txt file before using, installing, copying,
 * modifying or distribute this file or part of its contents. The contents of
 * this file is part of the Source Code of CKFinder.
 */

using System;
using System.Web;
using System.Xml;
using System.Globalization;
using System.Collections;
using System.Text.RegularExpressions;

namespace CKFinder.Connector.CommandHandlers
{
	internal class MoveFilesCommandHandler : XmlCommandHandlerBase
	{
		private XmlNode ErrorsNode;

		public MoveFilesCommandHandler()
			: base()
		{
		}

		private void appendErrorNode( int errorCode, string name, string type, string path )
		{
			if ( this.ErrorsNode == null )
				this.ErrorsNode = XmlUtil.AppendElement( this.ConnectorNode, "Errors" );

			XmlNode Error = XmlUtil.AppendElement( this.ErrorsNode, "Error" );
			XmlUtil.SetAttribute( Error, "code", errorCode.ToString() );
			XmlUtil.SetAttribute( Error, "name", name );
			XmlUtil.SetAttribute( Error, "type", type );
			XmlUtil.SetAttribute( Error, "folder", path );
		}

		protected override void BuildXml()
		{
			if ( Request.Form["CKFinderCommand"] != "true" )
			{
				ConnectorException.Throw( Errors.InvalidRequest );
			}

			if ( !this.CurrentFolder.CheckAcl( AccessControlRules.FileRename | AccessControlRules.FileUpload | AccessControlRules.FileDelete ) )
			{
				ConnectorException.Throw( Errors.Unauthorized );
			}

			if ( Request.Form["files[0][type]"] == null )
			{
				ConnectorException.Throw( Errors.InvalidRequest );
			}

			int moved = 0;
			int movedAll = 0;
			if ( Request.Form["moved"] != null )
			{
				movedAll = Int32.Parse( Request.Form["moved"] );
			}
			Settings.ResourceType resourceType;
			Hashtable resourceTypeConfig = new Hashtable();
			Hashtable checkedPaths = new Hashtable();

			int iFileNum = 0;
			while ( Request.Form["files[" + iFileNum.ToString() + "][type]"] != null && Request.Form["files[" + iFileNum.ToString() + "][type]"].Length > 0 )
			{
				string name = Request.Form["files[" + iFileNum.ToString() + "][name]"];
                name = name.Replace(" ", "-");
                string type = Request.Form["files[" + iFileNum.ToString() + "][type]"];
				string path = Request.Form["files[" + iFileNum.ToString() + "][folder]"];
				string options = "";

				if ( name == null || name.Length < 1 || type == null || type.Length < 1 || path == null || path.Length < 1 )
				{
					ConnectorException.Throw( Errors.InvalidRequest );
					return;
				}

				if ( Request.Form["files[" + iFileNum.ToString() + "][options]"] != null )
				{
					options = Request.Form["files[" + iFileNum.ToString() + "][options]"];
				}
				iFileNum++;

				// check #1 (path)
				if ( !Connector.CheckFileName( name ) || Regex.IsMatch( path, @"(/\.)|(\.\.)|(//)|([\\:\*\?""\<\>\|])" ) )
				{
					ConnectorException.Throw( Errors.InvalidRequest );
					return;
				}

				// get resource type config for current file
				if ( !resourceTypeConfig.ContainsKey( type ) )
				{
					resourceTypeConfig[type] = Config.Current.GetResourceTypeConfig( type );
				}

				// check #2 (resource type)
				if ( resourceTypeConfig[type] == null )
				{
					ConnectorException.Throw( Errors.InvalidRequest );
					return;
				}

				resourceType = (Settings.ResourceType)resourceTypeConfig[type];
				FolderHandler folder = new FolderHandler( type, path );
				string sourceFilePath = System.IO.Path.Combine( folder.ServerPath, name );

				// check #3 (extension)
				if ( !resourceType.CheckExtension( System.IO.Path.GetExtension( name ) ) )
				{
					this.appendErrorNode( Errors.InvalidExtension, name, type, path );
					continue;
				}

				// check #4 (extension) - when moving to another resource type, double check extension
				if ( this.CurrentFolder.ResourceTypeName != type )
				{
					if ( !this.CurrentFolder.ResourceTypeInfo.CheckExtension( System.IO.Path.GetExtension( name ) ) )
					{
						this.appendErrorNode( Errors.InvalidExtension, name, type, path );
						continue;
					}
				}

				// check #5 (hidden folders)
				if ( !checkedPaths.ContainsKey( path ) )
				{
					checkedPaths[path] = true;
					if ( Config.Current.CheckIsHidenPath( path ) )
					{
						ConnectorException.Throw( Errors.InvalidRequest );
						return;
					}
				}

				// check #6 (hidden file name)
				if ( Config.Current.CheckIsHiddenFile( name ) )
				{
					ConnectorException.Throw( Errors.InvalidRequest );
					return;
				}

				// check #7 (Access Control, need file view permission to source files)
				if ( !folder.CheckAcl( AccessControlRules.FileView ) )
				{
					ConnectorException.Throw( Errors.Unauthorized );
					return;
				}

				// check #8 (invalid file name)
				if ( !System.IO.File.Exists( sourceFilePath ) || System.IO.Directory.Exists( sourceFilePath ) )
				{
					this.appendErrorNode( Errors.FileNotFound, name, type, path );
					continue;
				}

				// check #9 (max size)
				if ( this.CurrentFolder.ResourceTypeName != type )
				{
					System.IO.FileInfo fileInfo = new System.IO.FileInfo( sourceFilePath );
					if ( this.CurrentFolder.ResourceTypeInfo.MaxSize > 0 && fileInfo.Length > this.CurrentFolder.ResourceTypeInfo.MaxSize )
					{
						this.appendErrorNode( Errors.UploadedTooBig, name, type, path );
						continue;
					}
				}

				string destinationFilePath = System.IO.Path.Combine( this.CurrentFolder.ServerPath, name );
				string thumbPath = System.IO.Path.Combine( folder.ThumbsServerPath, name );

				// finally, no errors so far, we may attempt to copy a file
				// protection against copying files to itself
				if ( sourceFilePath == destinationFilePath )
				{
					this.appendErrorNode( Errors.SourceAndTargetPathEqual, name, type, path );
					continue;
				}
				// check if file exists if we don't force overwriting
				else if ( System.IO.File.Exists( destinationFilePath ) )
				{
					if ( options.Contains( "overwrite" ) )
					{
						try
						{
							System.IO.File.Delete( destinationFilePath );
						}
						catch ( Exception )
						{
							this.appendErrorNode( Errors.AccessDenied, name, type, path );
							continue;
						}
						try
						{
							System.IO.File.Move( sourceFilePath, destinationFilePath );
							moved++;

							try
							{
								System.IO.File.Delete( thumbPath );
							}
							catch { /* No errors if we are not able to delete the thumb. */ }
						}
						catch ( Exception )
						{
							this.appendErrorNode( Errors.AccessDenied, name, type, path );
							continue;
						}
					}
					else if ( options.Contains( "autorename" ) )
					{
						int iCounter = 1;
						string fileName;
						string sFileNameNoExt = CKFinder.Connector.Util.GetFileNameWithoutExtension( name );
						string sFullExtension = CKFinder.Connector.Util.GetExtension( name );
						while ( true )
						{
							fileName = sFileNameNoExt + "(" + iCounter.ToString() + ")" + sFullExtension;
							destinationFilePath = System.IO.Path.Combine( this.CurrentFolder.ServerPath, fileName );
							if ( !System.IO.File.Exists( destinationFilePath ) )
								break;
							else
								iCounter++;
						}
						try
						{
							System.IO.File.Move( sourceFilePath, destinationFilePath );
							moved++;

							try
							{
								System.IO.File.Delete( thumbPath );
							}
							catch { /* No errors if we are not able to delete the thumb. */ }
						}
						catch ( ArgumentException )
						{
							this.appendErrorNode( Errors.InvalidName, name, type, path );
							continue;
						}
						catch ( System.IO.PathTooLongException )
						{
							this.appendErrorNode( Errors.InvalidName, name, type, path );
							continue;
						}
						catch ( Exception )
						{
#if DEBUG
							throw;
#else
							this.appendErrorNode( Errors.AccessDenied, name, type, path );
							continue;
#endif
						}
					}
					else
					{
						this.appendErrorNode( Errors.AlreadyExist, name, type, path );
						continue;
					}
				}
				else
				{
					try
					{
						System.IO.File.Move( sourceFilePath, destinationFilePath );
						moved++;

						try
						{
							System.IO.File.Delete( thumbPath );
						}
						catch { /* No errors if we are not able to delete the thumb. */ }
					}
					catch ( ArgumentException )
					{
						this.appendErrorNode( Errors.InvalidName, name, type, path );
						continue;
					}
					catch ( System.IO.PathTooLongException )
					{
						this.appendErrorNode( Errors.InvalidName, name, type, path );
						continue;
					}
					catch ( Exception )
					{
#if DEBUG
							throw;
#else
						this.appendErrorNode( Errors.AccessDenied, name, type, path );
						continue;
#endif
					}
				}
			}

			XmlNode moveFilesNode = XmlUtil.AppendElement( this.ConnectorNode, "MoveFiles" );
			XmlUtil.SetAttribute( moveFilesNode, "moved", moved.ToString() );
			XmlUtil.SetAttribute( moveFilesNode, "movedTotal", ( movedAll + moved ).ToString() );

			if ( this.ErrorsNode != null )
			{
				ConnectorException.Throw( Errors.MoveFailed );
				return;
			}
		}
	}
}
