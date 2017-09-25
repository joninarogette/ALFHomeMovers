$(document).ready(function(){
    $("#reset").click(function(){
        $('#Customer').val("");
        $('#Contact').val("");
        $('#Address').val("");
        $('#Destination').val("");
        $('#sched').val("");
        });

    $("#clear").click(function(){
        $('#FName').val("");
        $('#LName').val("");
        $('#Contact').val("");
    });

    $("#void").click(function(){
        $('#plate').val("");
        $('#model').val("");
        $('#capacity').val("");
        $('#date').val("");
    });

});

