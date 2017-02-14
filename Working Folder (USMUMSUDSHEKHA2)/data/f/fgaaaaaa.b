$(document).ready(function () {
    $(function () {
        pageGrids.ordersGrid.onRowSelect(function (e) {
            $("#hdnSessionId").val(e.row.SessionId);
            $("#txtSessionName").val(e.row.Session);
            $("#cmbUproc").val(e.row.UprocId);
            $("#cmbEnvironment").val(e.row.EnvironmentID);
            $("#cmbAccount").val(e.row.AccountTypeID);
            $("#hdnNode").val(e.row.Node);
        });
        $('#btnCancel').click(function () {
            $.ajax({
                url: '@Url.Content("~/Home}/Index")',
                type: 'GET'
            });
        });
    });
});

