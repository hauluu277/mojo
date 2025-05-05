<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="GalleryModule.ascx.cs" Inherits="MediaFeature.UI.GalleryModule" %>



<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <mp:CornerRounderTop ID="ctop1" runat="server" />
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper Document">
        <portal:ModuleTitleControl runat="server" ID="TitleControl" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
            <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent container">
                <asp:Panel ID="pnlTabLightbox" runat="server">

                    <link href="/Data/plugins/ionicons/css/grid-gallery.min.css" rel="stylesheet" />
                    <script src="/Data/plugins/ionicons/js/grid-gallery.js"></script>
                    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fancybox/3.5.7/jquery.fancybox.min.css" />
                    <div class="content">
                        <div class="gg-container">
                            <div class="gg-box dark">
                                <asp:Repeater ID="rptGalleryLightbox" runat="server">
                                    <ItemTemplate>

                                        <img class="gg-anh gg-lightbox   " src="<%#Eval("FilePath") %>" alt="<%#Eval("GroupName") %>" />

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>




                    <script>
                        $(document).ready(function () {
                            gridGallery({
                                selector: ".dark",
                                darkMode: true
                            });
                        });

                        document.addEventListener("DOMContentLoaded", function () {
                            const images = document.querySelectorAll(".gg-anh.gg-lightbox");
                            const lightboxContainer = document.createElement("div");
                            lightboxContainer.classList.add("lightbox-container");
                            document.body.appendChild(lightboxContainer);
                            let currentIndex = 0;


                            function showImageInLightbox(imageIndex) {
                                const lightbox = document.createElement("div");
                                lightbox.classList.add("lightbox");

                                const image = document.createElement("img");
                                image.src = images[imageIndex].src;
                                image.alt = images[imageIndex].alt;

                                const prevButton = document.createElement("span");
                                prevButton.classList.add("prev-button");
                                prevButton.innerHTML = "&#10094;";
                                prevButton.addEventListener("click", showPrevImage);

                                const nextButton = document.createElement("span");
                                nextButton.classList.add("next-button");
                                nextButton.innerHTML = "&#10095;";
                                nextButton.addEventListener("click", showNextImage);

                                const closeButton = document.createElement("span");
                                closeButton.classList.add("close-button");
                                closeButton.innerHTML = "&times;";
                                closeButton.addEventListener("click", closeLightbox);

                                lightbox.appendChild(image);
                                lightbox.appendChild(prevButton);
                                lightbox.appendChild(nextButton);
                                lightbox.appendChild(closeButton);

                                lightboxContainer.innerHTML = "";
                                lightboxContainer.appendChild(lightbox);

                                currentIndex = imageIndex;
                                lightboxContainer.style.display = "flex";
                            }


                            function closeLightbox() {
                                lightboxContainer.style.display = "none";
                            }


                            function showPrevImage() {
                                currentIndex = (currentIndex - 1 + images.length) % images.length;
                                showImageInLightbox(currentIndex);
                            }


                            function showNextImage() {
                                currentIndex = (currentIndex + 1) % images.length;
                                showImageInLightbox(currentIndex);
                            }

                            images.forEach((image, index) => {

                                image.addEventListener("click", () => showImageInLightbox(index));
                            });
                        });



                    </script>



                </asp:Panel>
                <asp:Panel ID="pnlGallerySlider" runat="server">
                    <script src="/Data/plugins/lightbox2/js/lightslider.js"></script>

                    <div class="demo container" id="content-slider">
                        <div class="item">
                            <div class="clearfix">
                                <ul id="image-gallery" class="gallery list-unstyled cS-hidden">
                                    <asp:Repeater ID="rptGallerySlider" runat="server">
                                        <ItemTemplate>
                                            <li data-thumb="<%#Eval("FilePath")%>" class="item">
                                                <img src="<%#Eval("FilePath")%>" />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <script>
                        $(document).ready(function () {
                            //$("#content-slider").lightSlider({
                            //    loop: true,
                            //    keyPress: true,
                            //});

                            var item =<%=config.NumberShowSetting%>;
                            if (item == 0) {
                                item = 100;
                            }
                            $("#image-gallery").lightSlider({
                                gallery: true,
                                item: 1,
                                thumbItem: item,
                                slideMargin: 0,
                                speed: <%=config.ThoiGianChaySetting%>,
                                auto: true,
                                loop: true,
                                onSliderLoad: function () {
                                    $("#image-gallery").removeClass("cS-hidden");
                                },
                            });
                        });
                    </script>
                </asp:Panel>
            </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel ID="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
    <mp:CornerRounderBottom ID="cbottom1" runat="server" />
</portal:OuterWrapperPanel>
