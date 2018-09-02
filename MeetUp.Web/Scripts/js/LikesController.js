﻿var LikesController = (function () {
    var likeBtnClass = "likeBtn";
    var likesController = {};

    likesController.init = function () {
        return this;
    }

    likesController.handleLikes = function () {
        $("." + likeBtnClass).on("click", function (ev) {
            var target = ev.target;
            var likedUserIdString = target.dataset.user_id;
            var likedUserIdInt = (likedUserIdString != "undefined" && likedUserIdString != null && likedUserIdString.length > 0)
                ? parseInt(likedUserIdString)
                : 0;

            $.ajax({
                method: "POST",
                url: "/Home/LikeUser",
                data: { id: likedUserIdInt }
            })
            .done(function (resp) {
                if (resp.success)
                {
                    var text = $("#likeBtn_" + likedUserIdInt).html() == "Like" ? "Dislike" : "Like";
                    $("#likeBtn_" + likedUserIdInt).html(text);

                    // Removing the user div in WhoILike and WhoLikesMe pages
                    $("#userDiv_" + likedUserIdInt).remove();
                }
            });
        })
    }

    return likesController;
}());


var lc = Object.create(LikesController).init().handleLikes();
