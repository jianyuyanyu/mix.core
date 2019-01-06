'use strict';
app.constant('ngAppSettings', {
        serviceBase: '',
        clientId: 'ngAuthApp',
        facebookAppId: '464285300363325',
        request: {
            pageSize: '10',
            pageIndex: 0,
            status: 'Published',
            orderBy: 'Priority',
            direction: '0',
            fromDate: null,
            toDate: null,
            keyword: ''
        },
        pageSizes: [
            '5',
            '10',
            '15',
            '20'
        ],
        directions: [
            {
                value: '0',
                title: 'Asc'
            },
            {
                value: '1',
                title: 'Desc'
            }
        ],
        orders: [
            {
                value: 'CreatedDateTime',
                title: 'Created Date'
            }
            ,
            {
                value: 'Priority',
                title: 'Priority'
            },

            {
                value: 'Title',
                title: 'Title'
            }
        ],
        contentStatuses: [
            'Deleted',
            'Preview',
            'Published',
            'Draft',
            'Schedule'
        ],
        dataTypes: [
            { title: 'string', value: 0 },
            { title: 'int', value: 1 },
            { title: 'image', value: 2 },
            { title: 'codeEditor', value: 4 },
            { title: 'html', value: 5 },
            { title: 'textArea', value: 6 },
            { title: 'boolean', value: 7 },
            { title: 'mdTextArea', value: 8 },
            { title: 'date', value: 9 },
            { title: 'datetime', value: 10 }
        ],
        miIcons: [
            'mi-Page','mi-AllApps', 'mi-Add'
        ]  
    });

function Filter($filter) {
    return function (utcDateString, format) {
        // return if input date is null or undefined
        if (!utcDateString) {
            return;
        }

        // append 'Z' to the date string to indicate UTC time if the timezone isn't already specified
        if (utcDateString.indexOf('Z') === -1 && utcDateString.indexOf('+') === -1) {
            utcDateString += 'Z';
        }

        // convert and format date using the built in angularjs date filter
        return $filter('date')(utcDateString, format);
    };
}