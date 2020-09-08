var RelationshipService = function () {
    var createRelationship = function (done, fail, followerId) {
        $.post("/api/relationships/follow", { FollowerId: followerId })
            .done(done)
            .fail(fail);
    }

    var deleteRelationship = function (done, fail, followerId) {
        $.ajax({
            url: "/api/relationships/unfollow/" + followerId,
            method: "DELETE"
        }).done(done)
            .fail(fail);
    }

    return {
        createRelationship: createRelationship,
        deleteRelationship: deleteRelationship
    }
}();