var PolicyToDelete;

function ToDelete(id) {
    PolicyToDelete = id;
}

function DeletePolicy() {
    $.get("EmployeePositionTimeOffPolicies/Delete/" + PolicyToDelete)
        .done(function () {
            alert("Delete successful");
            var elem = "element" + PolicyToDelete;
            $("#" + elem).remove();
        })
        .fail(function () {
            alert("Delete comand error");
        })
}