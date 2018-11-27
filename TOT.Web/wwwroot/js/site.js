// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


function FiredWorkingSwitch() {
    var el = $("#Fired");
    var val = el.val();
    if (val == "True") {
        el.val("False");
        $("#FiredShow").text("Working");
    } else {
        el.val("True");
        $("#FiredShow").text("Fired");
    }
}