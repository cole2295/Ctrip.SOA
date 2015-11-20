app.service("userService", function ($http) {

    //get All users
    this.getUsers = function () {
        return $http.get("User/GetAllUsers");
    };

    // get User By Id
    this.getUser = function (userId) {
        var response = $http({
            method: "post",
            url: "User/GetUserById",
            params: {
                userId: JSON.stringify(userId)
            }
        });
        return response;
    }

    // Update User 
    this.UpdateUser = function (user) {
        var response = $http({
            method: "post",
            url: "User/UpdateUser",
            data: JSON.stringify(user),
            dataType: "json"
        });
        return response;
    }

    // Add User
    this.AddUser = function (user) {
        var response = $http({
            method: "post",
            url: "User/AddUser",
            data: JSON.stringify(user),
            dataType: "json"
        });
        return response;
    }

    //Delete User
    this.DeleteUser = function (userId) {
        var response = $http({
            method: "post",
            url: "User/DeleteUser",
            params: {
                userId: JSON.stringify(userId)
            }
        });
        return response;
    }
});