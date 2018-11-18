var AprArray = {};

function AddApprover() {
    var AprPost = $("#ApproverPosition");
    var AprAmoun = $("#ApproversAmoun").val();
    var AprList = $("#ApproversList");

    if (AprArray[AprPost.val()] !== undefined) {
        alert("It is not posible to add approvers whith same Positions");
        return;
    }

    AprArray[AprPost.val()] = AprAmoun;
    $("#Approvers").val(JSON.stringify(AprArray));
    AprList.append("<div class='text - danger' id='appr" + AprPost.val() + "'>" + AprPost.text() + " : " + AprAmoun +
        "<input type='button' value='X' class='btn btn -default' onclick='DeliteEle(" + AprPost.val() + ")' />" + "</div>");
}

function DeliteEle(id) {
    delete AprArray[id];
    $("#Approvers").val(JSON.stringify(AprArray));
    $("#appr" + id).remove();
}