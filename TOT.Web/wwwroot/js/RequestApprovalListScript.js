     
        $(document).ready(function () {
        var id = $("#selectList").val();

            if (id != null) {
                loadApprovalsList(id);
            }
        });

    $("#selectList").change(function () {
        var id = $("#selectList").val();
        loadApprovalsList(id);
    });

    function loadApprovalsList(id) {
            $('#partial').html("<center>Approvals are loading</center>")

            $.ajax({
                type: 'GET',
            url: '../RequestApproval/PartialAsync/' + id,
                data: {listId: id },
                success: function (result) {
                $('#partial').html(result)
            },
                error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }

