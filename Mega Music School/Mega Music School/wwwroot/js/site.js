//ADMIN POST VIDEOS THAT IS ACCEPTED
function AcceptVideoPost(Id) {
    debugger;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/AcceptVideos',
        data:
        {
            videoId: Id
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                debugger;
                successAlert(result.msg)
                window.location.reload();
            }
            else {
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            errorAlert(ex);
        }
    });
}

// ADMIN DELETE VIDEO THAT IS NOT ACCEPTED
function RejectVideoPost(Id) {
    debugger;
    $.ajax({
        type: 'POST',
        dataType: 'Json',
        url: '/Admin/RejectVideos',
        data:
        {
            videoId: Id
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                successAlert(result.msg)
                window.location.reload();
            }
            else {
                errorAlert(result.msg)
            }
        },
        Error: function (ex) {
            errorAlert(ex);
        }
    });
}