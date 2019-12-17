class Comment {
    constructor(name, text) {
        this.name = name;
        this.created = 'секунду назад';
        this.text = text;
        this.likes = 0;
        this.dislikes = 0;
    }
}

function AppCommentModel() {
    let self = this;
    self.comments = ko.observableArray([]);
    self.answerBoxVisibe = ko.observable(false);

    self.addComment = function (postId, parentCommentId = null) {
        //const text = parentCommentId ?
        //    $(`#${parentCommentId}-comment-box textarea`).val() :
        //    $('#comment-box-root textarea').val();

        const text = parentCommentId ?
            $(`.comment-answers textarea:visible`).val() :
            $('#comment-box-root textarea').val();

        $.ajax({
            url: '/comment/addcomment',
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify({
                Text: text,
                PostId: postId,
                ParentCommentId: parentCommentId
            }),
            success: (data) => {
                document.location.reload(true);
            },
            error: (error) => {
                console.log(error);
            }
        });
    };

    self.closeAllAnswerCommentBox = function () {
        $('div[id$="comment-box"] textarea').val('');
        $('div[id$="comment-box"]').hide('fast');
    };

    self.displayAnswerCommentBox = function (commentId, userName) {
        self.closeAllAnswerCommentBox();
        if (userName) $(`#${commentId}-comment-box textarea`).val('@' + userName + ', ');
        $(`#${commentId}-comment-box`).show('fast');
    };

    self.setReaction = function (commentId, parentCommentId, currentReaction) {
        $.ajax({
            url: '/comment/setcommentreaction',
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify({
                ParentCommentId: parentCommentId,
                CommentId: commentId,
                Liked: currentReaction
            }),
            success: (data) => {
                $(`#${commentId}-likes .comment-likes`).text(data.likes);
                $(`#${commentId}-dislikes .comment-dislikes`).text(data.dislikes);
                $(`#${commentId}-likes`).removeClass();
                $(`#${commentId}-dislikes`).removeClass();

                if (data.reaction === 1) {
                    $(`#${commentId}-likes`).addClass('noReaction');
                    $(`#${commentId}-dislikes`).addClass('noReaction');
                }
                else if (data.reaction === 2) {
                    $(`#${commentId}-likes`).addClass("liked");
                    $(`#${commentId}-dislikes`).addClass('noReaction');
                }
                else if (data.reaction === 3) {
                    $(`#${commentId}-dislikes`).addClass("disliked");
                    $(`#${commentId}-likes`).addClass('noReaction');
                }
            },
            error: (error) => {
                if (error.status === 403) {
                    $('#authorizeModal').modal('show');
                }
            }
        });
    };
}

ko.applyBindings(new AppCommentModel(), document.getElementById('post-comments-form'));