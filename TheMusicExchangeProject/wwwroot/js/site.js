// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let currentGroupId = null;

var pusher = new Pusher('a334995a12f8ba424958', {
    cluster: 'eu',
    encrypted: true
});

var channel = pusher.subscribe('group_chat');
channel.bind('new_group', function (data) {
    reloadGroup();
});
channel.bind('delete_group', function (data) {
    reloadGroup();
});

$('input.chk_class').on('change', function () {
    $('input.chk_class').not(this).prop('checked', false);
});

$("#CreateNewGroupButton").click(function () {
    let UserNames = $("input[name='UserName[]']:checked")
        .map(function () {
            return $(this).val();
        }).get();

    let data = {
        GroupName: $("#GroupName").val(),
        UserNames: UserNames
    };

    $.ajax({
        type: "POST",
        url: "/api/group",
        data: JSON.stringify(data),
        success: (data) => {
            $('#CreateNewGroup').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        },
        dataType: 'json',
        contentType: 'application/json'
    });

});

function deleteGroup(form) {
    $(form).parents('div').remove();
}

// When a user clicks on a group, Load messages for that particular group.
    $("#groups").on("click", ".group", function(){
        let group_id = $(this).attr("data-group_id");

        $('.group').css({"border-style": "none", cursor:"pointer"});
        $(this).css({"border-style": "inset", cursor:"default"});

        $("#currentGroup").val(group_id); // update the current group_id to a html form...
        currentGroupId =  group_id;

        // get all messages for the group and populate it...
        $.get( "/api/message/"+group_id, function( data ) {
            let message = "";

            data.forEach(function(data){
                var event = new Date(data.timeSent);
                let position = (data.addedBy == $("#UserName").val()) ? " float-right" : "";
                message += `<div class="row chat_message` + position + `">
                                <b>` + event.toLocaleTimeString('en-US') + data.addedBy + `: </b>` + data.message + ` 
                            </div>`;
            });

            $(".chat_body").html(message);
        });
        if( !pusher.channel('private-'+group_id) ){ // check the user have subscribed to the channel before.
            let group_channel = pusher.subscribe('private-'+group_id);

            group_channel.bind('new_message', function(data) { 
                 if( currentGroupId == data.new_message.GroupId){

                      $(".chat_body").append(`<div class="row chat_message"><b>`+ data.new_message.AddedBy +`: </b>`+ data.new_message.message +` </div>`);
                 }
              });  
        }
    });

$("#SendMessage").click(function () {
    $.ajax({
        type: "POST",
        url: "/api/message",
        data: JSON.stringify({
            AddedBy: $("#UserName").val(),
            GroupId: $("#currentGroup").val(),
            message: $("#Message").val(),
            socketId: pusher.connection.socket_id
        }),
        success: (data) => {
            var event = new Date(data.data.timeSent);
            $(".chat_body").append(`<div class="row chat_message float-right"><b>`
                + event.toLocaleTimeString('en-US') + data.data.addedBy + `: </b>` + $("#Message").val() + `</div>`
            );

            $("#Message").val('');
        },
        dataType: 'json',
        contentType: 'application/json'
    });
});

function reloadGroup() {
    $.get("/api/group", function (data) {
        let groups = "";

        data.forEach(function (group) {
            groups += `<div class="group" data-group_id="`
                + group.groupId + `">` + group.groupName +
                `<img src="` + `/Chat/ChatProfilePictures?GroupId=` + group.groupId + `" class="profile-pic" />` +
                `<form asp-controller="Chat" asp-action="Delete" asp-route-id="` + group.groupId + `" 
                    data-ajax="true" data-ajax-success="deleteGroup(this)">
                    <button type="submit" class="btn btn-secondary">Delete</button>
                </form>` +
                `</div>`;
        });

        $("#groups").html(groups);
    });
}

function showhideRequests() {
    var div = document.getElementById("requests");
    div.classList.toggle('hidden');
}
function showhideConnections() {
    var div = document.getElementById("connections");
    div.classList.toggle('hidden');
}
function showhideBlocks() {
    var div = document.getElementById("blocks");
    div.classList.toggle('hidden');
}
function showhideSearch() {
    var div = document.getElementById("search");
    var btn = document.getElementById("arrow")
    div.classList.toggle('hidden');
    btn.classList.toggle('open');
}