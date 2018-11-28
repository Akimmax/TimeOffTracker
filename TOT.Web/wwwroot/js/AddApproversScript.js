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
    AprList.append(`<div class="row list-group-item" id="appr${ AprPost.val()}"><div class="col-xs-11"><h4>${ AprText } : ${ AprAmoun }<h4></div>
        <input type="button" value="X" class="col-xs-1 btn btn-danger" onclick="DeliteEle(${ AprPost.val() })" /></div>`);
}

function ShowApr(){
    AprArray = JSON.parse($("#Approvers").val());
    var AprList = $("#ApproversList");
    PositionList = $("#ApproverPosition");
    for (var key in AprArray) {
        var AprText = $("#ApproverPosition").find('option[value="' + key + '"]').text();
        AprList.append(`<div class="row list-group-item" id="appr${ key }"><div class="col-xs-11"><h4>${ AprText } : ${ AprArray[key] }</h4></div>
            <input type="button" value="X" class="col-xs-1 btn btn-danger" onclick="DeliteEle(${ key })" /></div>`);
    }
}

function DeliteEle(id) {
    delete AprArray[id];
    $("#Approvers").val(JSON.stringify(AprArray));
    $("#appr" + id).remove();
}