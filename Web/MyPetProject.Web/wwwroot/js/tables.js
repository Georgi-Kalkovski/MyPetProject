$(document).ready(function () {
    $('#kingdoms').DataTable({
        'columnDefs': [
            {
                'searchable': false,
                'targets': [6]
            },
        ]
    });
});

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

$(document).ready(function () {
    $('#subbreeds').DataTable({
        'columnDefs': [
            {
                'searchable': false,
                'targets': [4]
            },
        ]
    });
});

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
