
GetSocials();
let application = document.getElementById('application');
if (application) {
    new Vue({
        el: '#application',
        data: {
            name: '',
            email: '',
            message: '',
            isAgree: false,
            errorName: '',
            errorEmail: '',
            errorMessage: '',
            isSend: true,
        },
        methods: {
            send: function () {
                if (this.isAgree === true) {
                    let data = {};
                    data.Id = 0;
                    data.Name =  this.name;
                    data.Email = this.email;
                    data.Description = this.message;
                    data.Agree = this.isAgree;

                    fetch('/Home/Requestion', {
                        method: 'POST',
                        headers: new Headers({ "Content-Type": "application/json" }),
                        body: JSON.stringify(data)
                    }).then(responce => {
                        if (responce.ok) {
                            this.name = '';
                            this.email = '';
                            this.message = '';
                            this.isAgree = false;
                        }
                    });
                }
            },
            validEmail: function (email) {
                var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(email);
            },
            validation: function () {
                let result = false;
                if (this.name.length == 0) {
                    this.errorName = "Укажите имя имя.";
                    result = true;
                } else {
                    if (this.name.length <= 2 || this.name.length >= 150) {
                        this.errorName = "Поле не должно быть меньше 2 и больше 150 символов.";
                    } else {
                        this.errorName = '';
                    }
                }
                if (this.email.length == 0) {
                    this.errorEmail = 'Укажите электронную почту.';
                    result = true;
                } else if (!this.validEmail(this.email)) {
                    this.errorEmail = 'Укажите корректный адрес электронной почты.';
                    result = true;
                } else if (this.email.length >= 100) {
                    this.errorEmail = 'Поле не должно превышать 100 символов.';
                    result = true;
                }
                else {
                    this.errorEmail = '';
                }

                if (this.message.length >= 500 || this.message.length <= 10) {
                    this.errorMessage = 'Поле не должно быть меньше 10 и больше 500 символов.';
                    result = true;
                } else {
                    this.errorMessage = '';
                }

                if (this.isAgree == false) {
                    result = true;
                }

                this.isSend = result;
            }
        },
        watch: {
            name: function (val, oldVal) {
                this.validation();
            },
            email: function (val, oldVal) {
                this.validation();
            },
            message: function (val, oldVal) {
                this.validation();
            },
            isAgree: function (val, old) {
                this.validation();
            }
        }
    })
}

function GetSocials() {
    fetch("/Home/Socials").then(responce => {
        if (responce.ok) {
            return responce.json();
        }
    }).then(socials => {
        for (let i = 0; i < socials.length; i++) {
            socials[i].imageId = '/Files/' + socials[i].getImages.location + '/' + socials[i].getImages.name;
        }
        socVuew.socs = socials;
    });

    let socVuew =  new Vue({
        el: '#socialList',
        data: {
            socs: []
        }
    })
}

let service = document.getElementById('services');
if (service) {
    service.addEventListener("click", function (e) {
        let element = e.target;
        if (element.classList.contains('active')) {
            element.classList.remove('active');
            element.parentNode.classList.remove('active')
        } else {
            element.classList.add('active');
            element.parentNode.classList.add('active')
        }
    });
}
