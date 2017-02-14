$(document).ready(function () {
    $(function () {
        pageGrids.ordersGrid.onRowSelect(function (e) {
            $("#hdnUprocId").val(e.row.UprocID);
            $("#txtUprocName").val(e.row.UprocName);
            $("#cmbJobType").val(e.row.JobTypeID);
            $("#cmbEnvironment").val(e.row.EnvironmentID);
            $("#cmbAccountType").val(e.row.AccountTypeID);
            $("#txtFolderPath").val(e.row.FolderPath);
            $("#txtCommand").val(e.row.Command);
        });
        $('#btnCancel').click(function () {
            debugger;
            $.ajax({
                url: '@Url.Content("~/Home}/Index")',
                type: 'GET'
            });
        });
    });
});

