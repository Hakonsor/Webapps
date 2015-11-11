var App = angular.module("App", []);

App.controller("faqController", function ($scope, $http) {

    var url = '/api/FAQ/';
    $scope.sletteFeil = false;
    $scope.laster = true;
    function sporsmalListe() {
        $http.get(url).success(function (alleSporsmal) {
            $scope.alleSporsmal = alleSporsmal;
            $scope.laster = false;
        }).error(function (data, status) {
            console.log(status + data);
        });
    };

    
    $scope.emailPattern = "/^[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]@[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$/";
    
    
    start = function () {
        $scope.email = "";
        $scope.beskrivelse = "";
        $scope.FAQ = true;
        $scope.Sporsmal = false;
        $scope.Liste = false;
        $scope.kategori1 = true;
        $scope.kategori2 = false;
        $scope.kategori3 = false;
        $scope.kategori4 = false;
    };
    start();

    $scope.viskategori1 = function () {
 
        $scope.kategori1 = true;
        $scope.kategori2 = false;
        $scope.kategori3 = false;
        $scope.kategori4 = false;
    };

    $scope.viskategori2 = function () {
        $scope.kategori2 = true;
        $scope.kategori1 = false;
        $scope.kategori3 = false;
        $scope.kategori4 = false;
    };

    $scope.viskategori3 = function () {
        $scope.kategori3 = true;
        $scope.kategori2 = false;
        $scope.kategori1 = false;
        $scope.kategori4 = false;
    };

    $scope.viskategori4 = function () {
        $scope.kategori4 = true;
        $scope.kategori2 = false;
        $scope.kategori3 = false;
        $scope.kategori1 = false;
    };

    $scope.visSporsmal = function () {
        $scope.email = "";
        $scope.beskrivelse = "";
        $scope.FAQ = false;
        $scope.Sporsmal = true;
        $scope.Liste = false;
    };

    $scope.visFAQ = function () {
        $scope.email = "";
        $scope.beskrivelse = "";
        $scope.FAQ = true;
        $scope.Sporsmal = false;
        $scope.Liste = false;
        $scope.viskategori1();
    };

    $scope.visListe = function () {
        $scope.email = "";
        $scope.beskrivelse = "";
        $scope.Liste = true;
        $scope.FAQ = false;
        $scope.Sporsmal = false;
        sporsmalListe();
    };

    $scope.setTempid = function (id) {
        $scope.tempid = id;
        console.log(id);
    };

    $scope.lagreSporsmal = function () {
        var Spørsmal = {
            email: $scope.email,
            beskrivelse: $scope.beskrivelse
        };
        
        $http.post(url, Spørsmal).
            success(function (data) {
                sporsmalListe();
                $scope.laster = false;
                //vis knapper
            }).
        error(function (data, status) {
            console.log(status + data);
        });
    };
    

    $scope.sletteSporsmal = function (id) {
        $http.delete(url+id).
        success(function (data) {
            sporsmalListe();
            $tempId = 0;
        }).
        error(function (data, status) {
            console.log(status + data);
            $tempId = 0;
        });
    };
    $scope.sletteSporsmal = function () {
        $http.delete(url + $scope.tempid).
        success(function (data) {
            sporsmalListe();
            $tempId = 0;
        }).
        error(function (data, status) {
            console.log(status + data);
            $tempId = 0;
        });
    };
   
  


});