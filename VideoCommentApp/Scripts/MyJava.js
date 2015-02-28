var IDpend = 1;

$(document).ready(function () {
    
    $("#PostIt").click(function () {
        
        var TextToComment = $("#CommentText").val();
        var user = "";
        var date = $("#DateRightNOW").val();

        if (TextToComment === "") {
            alert("You can't post nothing! that's just silly");
            return;
        }
        

        $.post("/Home/Index", { Username: user, CommentText: TextToComment, CommentDate: date}, function (data) {
            var CorrectDate = moment(data.CommentDate).format('D. MMMM hh:mm');
            $("#commentList").empty();
            $("#commentList").loadTemplate($("#template"), data);
            
            $("#LikeButtonToLike").data("id", IDpend);
            IDpend++;

            //Posta ollum like'um a comment'id
            $.get("Home/getItLikes", function (data) {
                $("#ListOfLikes").loadTemplate($("#templateLike"), data);
            });

            //lætur text field'id vera tomt
            document.getElementById("CommentText").value = "";           
        });
    });


    $(document).on('click', '#LikeButtonToLike', function () {


        var user = $("#UserId").val();
        var date = $("#DateRightNOW").val();
        var elem = $(this);
        var parent = elem.parent();

        $.post("/Home/LikeIt", { LikeUsername: user, LikeDate: date }, function (data) {
            if (data === "")
            {
                alert("You already liked this!")
                return;
            }
            $("#ListOfLikes").empty();
            $("#ListOfLikes").loadTemplate($("#templateLike"), data).appendTo(parent);
             
        });
    });

    

    $.get("/Home/GetIt", function (data) {

        $("#commentList").loadTemplate($("#template"), data);
    });


    $.get("Home/getItLikes  ", function (data) {

        $("#ListOfLikes").loadTemplate($("#templateLike"), data);
    });

});

