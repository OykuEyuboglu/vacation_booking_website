const formAll = document.querySelector('.formAll');
const loginLink = document.querySelector('.login-link');
const registerLink = document.querySelector('.register-link');
const btnPopup = document.querySelector('.btnLogin-popup');
const iconClose = document.querySelector('.icon-close');
const body = document.body;

registerLink.addEventListener('click', () => {
    formAll.classList.add('active');
    clearFormInputs();
});

loginLink.addEventListener('click', () => {
    formAll.classList.remove('active');
    clearFormInputs();
});

btnPopup.addEventListener('click', () => {
    formAll.classList.add('active-popup');
    body.classList.add('popup-open');
});

iconClose.addEventListener('click', () => {
    formAll.classList.remove('active-popup');
    body.classList.remove('popup-open');
});


const inputElements = document.querySelectorAll('.inputInput');

inputElements.forEach(input => {
    if (input.value !== '') {
        input.classList.add('filled');
    }
});

inputElements.forEach(input => {
    input.addEventListener('input', () => {
        if (input.value !== '') {
            input.classList.add('filled');
        } else {
            input.classList.remove('filled');
        }
    });
});

function clearFormInputs() {
    inputElements.forEach(input => {
        input.value = '';
        input.classList.remove('filled');
    });
}

registerLink.addEventListener('click', () => {
    formAll.classList.add('active');
    clearFormInputs();
});

loginLink.addEventListener('click', () => {
    formAll.classList.remove('active');
    clearFormInputs();
});
