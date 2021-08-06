$(document).ready(function () {
    $('#breeds').DataTable(
        {
            'columnDefs': [
                {
                    'searchable': false,
                    'targets': [3]
                },
            ]
        });
});