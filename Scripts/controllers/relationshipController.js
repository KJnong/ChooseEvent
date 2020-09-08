var RelationshipController = function (relationshipService) {
    var button;
    

    var relationship = function () {
        $(".js-toggle-follow").click(function (e) {
            button = $(e.target);
            var followerId = button.attr("data-user-id");

            if (button.hasClass("btn-default")) {
                relationshipService.createRelationship(done, fail, followerId)
                
            } else {
                relationshipService.deleteRelationship(done, fail, followerId)
            }  
        });

     

        var done = function () {
            var text = button.text() == "Following" ? "Following?" : "Following";

            button.toggleClass("btn-default").toggleClass("btn-info").text(text);
        };

        var fail = function () {
            alert("Something failed!");
        }
    }
    return { followToggle: relationship }
}(RelationshipService);