﻿$(document).ready(function () {
    $(".menu_links").hover(
        function () {
            $(this).addClass("menu_hover");
        },
        function () {
            $(this).removeClass("menu_hover");
        });
    //класс удаляется при обновлении страницы, при сортировке записей по выьранной категории
    $(".menu_links").click(function () {
        //$("menu_links").each(function () {
        //    $(this).removeClass("menu_selected");
        //});
        $(this).addClass("menu_selected");
    });
});