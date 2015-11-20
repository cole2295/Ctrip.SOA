app.controller("usercontroller", function ($scope, userService) {

    $scope.showupdate = false;
    $scope.showadd = false;
    GetAllUsers();

    //To Get All Records  
    function GetAllUsers() {
    //GetAllUser 
        var getData = userService.getUsers();

        getData.then(function (user) {
            $scope.users = user.data;
        }, function () {
            alert('Error in getting records');
        });
    }

    $scope.showUpdateDiv = function (user) {

        $scope.showupdate = true;
        var getData = userService.getUser(user.UserId);
        getData.then(function (user) {
            $scope.user = {};
            $scope.user.userId = user.data.UserId;
            $scope.user.userName = user.data.UserName;
       
        },
        function () {
            alert('Error in getting records');
        });
    }

    $scope.showAddDiv = function () {
        $scope.showadd = true;
    }



    $scope.EditUser = function (user) {
         var getData = userService.UpdateUser(user);
            getData.then(function (msg) {
              
                if(msg.data.IsSuccess)
                {
                     alert("updated");
                     GetAllUsers();
                }
                else
                {
                     alert(msg.data.Msg);
                }
                //alert(msg.data);
                //$scope.divUser = false;
            }, function () {
                alert('Error in adding record');
            });
    }

    $scope.AddUser = function () {

        var User = {
            UserName: $scope.userName,
        };
        //var getAction = $scope.Action;

        var getData = userService.AddUser(User);
            getData.then(function (msg) {
              
                if(msg.data.IsSuccess)
                {
                     GetAllUsers();
                }
                else
                {
                     alert(msg.data.Msg);
                }
                //alert(msg.data);
                //$scope.divUser = false;
            }, function () {
                alert('Error in adding record');
            });
//        if (getAction == "Update") {
//            User.Id = $scope.userId;
//            var getData = userService.updateEmp(User);
//            getData.then(function (msg) {
//                GetAllUser();
//                alert(msg.data);
//                $scope.divUser = false;
//            }, function () {
//                alert('Error in updating record');
//            });
//        } else {
//           
//        }
    }

   

    $scope.deleteUser = function (user) {
        var getData = userService.DeleteUser(user.UserId);
        getData.then(function (msg) {
            GetAllUsers();
            alert('User Deleted');
        }, function () {
            alert('Error in Deleting Record');
        });
    }

    function ClearFields() {
        $scope.userId = "";
        $scope.userName = "";
        $scope.userEmail = "";
        $scope.userAge = "";
    }
});