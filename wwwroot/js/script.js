document.addEventListener("DOMContentLoaded", function () {
 
 var acc = document.getElementsByClassName("accordion");
        var i;

        for (i = 0; i < acc.length; i++) {
            acc[i].onclick = function(){
                this.classList.toggle("active");
                this.nextElementSibling.classList.toggle("show");
            }
        }

        class MobileNavbar {
            constructor(mobileMenu, navList, navLinks) {
                this.mobileMenu = document.querySelector(mobileMenu);
                this.navList = document.querySelector(navList);
                this.navLinks = document.querySelectorAll(navLinks);
                this.activeClass = "active";
                this.scrollThreshold = 2;

                this.handleClick = this.handleClick.bind(this);
                this.handleScroll = this.handleScroll.bind(this);
                this.lastScrollPosition = window.scrollY;
            }

            animateLinks() {
                this.navLinks.forEach((link, index) => {
                link.style.animation
                    ? (link.style.animation = "")
                    : (link.style.animation = `navLinkFade 0.5s ease forwards ${
                        index / 7 + 0.3
                    }s`);
                });
            }

            handleClick() {
                this.navList.classList.toggle(this.activeClass);
                this.mobileMenu.classList.toggle(this.activeClass);
                this.animateLinks();
            
                // Remover o manipulador de rolagem durante o menu aberto
                if (this.navList.classList.contains(this.activeClass)) {
                    window.removeEventListener("scroll", this.handleScroll);
                } else {
                    window.addEventListener("scroll", this.handleScroll);
                }
            }

            handleScroll() {
                const currentScrollPosition = window.scrollY;

                if (currentScrollPosition > this.lastScrollPosition + this.scrollThreshold) {
                    // Scroll para baixo
                    this.navList.classList.remove(this.activeClass);
                    this.mobileMenu.classList.remove(this.activeClass);
                }

                this.lastScrollPosition = currentScrollPosition;
            }

            addEvents() {
                this.mobileMenu.addEventListener("click", this.handleClick);
                window.addEventListener("scroll", this.handleScroll);
            }

            init() {
                if (this.mobileMenu) {
                    this.addEvents();
                }
                return this;
            }
            }

            const mobileNavbar = new MobileNavbar(
            ".mobile-menu",
            ".nav-list",
            ".nav-list li",
            );
            mobileNavbar.init();

            const handlePhone = (event) => {
                let input = event.target
                input.value = phoneMask(input.value)
            }

            const phoneMask = (value) => {
                if (!value) return ""
                    value = value.replace(/\D/g,'')
                    value = value.replace(/(\d{2})(\d)/,"($1) $2")
                    value = value.replace(/(\d)(\d{4})$/,"$1-$2")
                return value
            }
            
        }) 
          
