// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// site.js

// Скрипт для подтверждения удаления
document.addEventListener('DOMContentLoaded', function () {
    var deleteLinks = document.querySelectorAll('.delete-link');
    deleteLinks.forEach(function (link) {
        link.addEventListener('click', function (event) {
            if (!confirm('Вы уверены, что хотите удалить этот элемент?')) {
                event.preventDefault();
            }
        });
    });
});


/* global bootstrap: false */
(function () {
    'use strict'
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    tooltipTriggerList.forEach(function (tooltipTriggerEl) {
        new bootstrap.Tooltip(tooltipTriggerEl)
    })
})()
