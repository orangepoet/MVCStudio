/// <reference path="jquery/jquery-1.6.2-vsdoc.js" />



$datepicker = function () {
    $(".datepicker").datepicker({
        showOn: "button",
        buttonImage: "../../content/images/calendar.gif",
        buttonImageOnly: true,
        changeMonth: true,
        changeYear: true,
        dateFormat:"yy/mm/dd"
    });
};

$selectone = function (chk) {
    $(".list :checkbox").each(function () {
        if ($(this).attr("id") != $(chk).attr("id")) {
            $(this).attr("checked", false);
        }
    });
};

$selectAll = function () {
    $(".table :input[name=selectAll]").each(function () {
        $(this).click(function () {
            var checked = $(this).attr("checked");
            $(".table tr:gt(0) :checkbox").each(function () {
                $(this).attr("checked", !checked);
            });
        });
    });
};

//$select = function () {
//    $(".table tr").each(function () {
//        $(this).click(function () {
//            $chk = $(this).find(":checkbox");
//            $chk.attr("checked", !$chk.attr("checked"));
//        });
//    });
//    $(".table :checkbox").each(function () {
//        $(this).click(function () {
//            $(this).attr("checked", !$(this).attr("checked"));
//        });
//    });
//};

$autoComplete = function () {
    $("input[data-autocomplete]").each(function () {
        $(this).autocomplete({
            source: $(this).attr("data-autocomplete")
        });
    });
};

$(document).ready(function () {
    $datepicker();
    //$selectAll();
    $autoComplete();
});


function ActionOK() {
    alert("操作成功 :-)");
}

function ActionErr() {
    alert("操作失败 :-(");
}