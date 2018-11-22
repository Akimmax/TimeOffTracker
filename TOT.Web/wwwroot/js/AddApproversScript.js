var AprArray = {};

function AddApprover() {
    var AprPost = $("#ApproverPosition");
    var AprAmoun = $("#ApproversAmoun").val();
    var AprList = $("#ApproversList");

    if (AprArray[AprPost.val()] !== undefined) {
        alert("It is not posible to add approvers whith same Positions");
        return;
    }
    var AprText = $("#ApproverPosition").find('option[value="' + AprPost.val() + '"]').text();
    AprArray[AprPost.val()] = AprAmoun;
    $("#Approvers").val(JSON.stringify(AprArray));
    AprList.append("<li class='list-group-item' id='appr" + AprPost.val() + "'>" + AprText + " : " + AprAmoun +
        "<input type='button' value='X' class='btn btn -default' onclick='DeliteEle(" + AprPost.val() + ")' />" + "</li>");
}

function ShowApr(){
    AprArray = JSON.parse($("#Approvers").val());
    var AprList = $("#ApproversList");
    PositionList = $("#ApproverPosition");
    for (var key in AprArray) {
        var AprText = $("#ApproverPosition").find('option[value="' + key + '"]').text();
        AprList.append("<div class='list-group-item' id='appr" +key+"'>"+AprText+" : " + AprArray[key] +
            "<input type='button' value='X' class='btn btn -default' onclick='DeliteEle("+key+")' /></div>");
    }
}

function DeliteEle(id) {
    delete AprArray[id];
    $("#Approvers").val(JSON.stringify(AprArray));
    $("#appr" + id).remove();
}