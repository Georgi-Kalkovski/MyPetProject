$(document).ready(function () {
    $('#foodtypes').DataTable(
        {
            'columnDefs': [
                {
                    'searchable': false,
                    'targets': [2]
                },
            ]
        });
});