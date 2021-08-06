$(document).ready(function () {
    $('#foods').DataTable(
        {
            'columnDefs': [
                {
                    'searchable': false,
                    'targets': [3]
                },
            ]
        });
});