var App = angular.module("App", []);

App.controller("faqController", function ($scope, $http) {

    var url = '/api/FAQ/';
    $scope.sletteFeil = false;

    function sporsmalListe() {
        $http.get(url).success(function (alleSporsmal) {
            $scope.sporsmal = alleSporsmal;
            $scope.laster = false;
        }).error(function (data, status) {
            console.log(status + data);
        });
    };
    $scope.visFAQ = true;
    $scope.visKnapp = true;
    $scope.laster = true;
    sporsmalListe();

    $scope.visRegSporsmal = function () {
        $scope.epost = "";
        $scope.beskrivelse = "";

        $scope.setPristine();
        $scope.regKnapp = false;
        $scope.visSkjema = true;
        $scope.visKunder = false;
        $scope.sendKnapp = true;
        $scope.oppdatering = false;
        //vis knapper
    }

    $scope.lagreSporsmal = function () {
        var sporsmal = {
            epost: $scope.epost,
            beskrivelse: $scope.beskrivelse
        };

        $http.post(url, sporsmal).
            success(function (data) {
                sporsmalListe();
                $scope.visFAQ;
                //vis knapper
            }).
        error(function (data, status) {
            console.log(status + data);
        });

        $scope.sletteSporsmal = function (id) {
            $http.delete(url + id).
            success(function (data) {
                sporsmalListe();

            }).
            error(function (data, status) {
                console.log(status + data);
            });
        };

    }

});