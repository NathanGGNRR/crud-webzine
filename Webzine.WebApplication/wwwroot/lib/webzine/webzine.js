$(document).ready(function () {

    $("#numberPage").change(function () {
        if ($(this).val() > 1 && $(this).val() != "") {
           
            if (parseInt($(this).val()) > parseInt($(this).attr('max'))) {
                $(this).val($(this).attr('max'));
                $("#btnGO").attr('href', "/Administration/" + $("#btnGO").data("controller") + "/page/" + $(this).attr('max'));
            } else {
                $("#btnGO").attr('href', "/Administration/" + $("#btnGO").data("controller") + "/page/" + $(this).val());
            }
            
        } else {
            if ($(this).val() < $(this).attr('min')) {
                $(this).val($(this).attr('min'));
            }
            $("#btnGO").attr('href', "/Administration/" + $("#btnGO").data("controller"));
        }
        
    });
    
});