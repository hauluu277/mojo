using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Linq;

namespace mojoPortal.Service.Business
{
    public class RoleBusiness : BaseBusiness<mp_Roles>
    {
        public RoleBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public bool HasRole(string roleName, int userId)
        {
            var getRole = this.context.mp_Roles.Where(x => x.RoleName.Equals(roleName)).FirstOrDefault();
            if (getRole != null)
            {
                var result = this.context.mp_UserRoles.Where(x => x.RoleID == getRole.RoleID && x.UserID == userId).Any();
                return result;
            }

            return false;
        }


    }
}

