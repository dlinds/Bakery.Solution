function CreateClickHandlersForEditDelete(id) {
  $(`form#editForm-${id}`).submit(function (event) {
    event.preventDefault();
    $.ajax({
      type: "POST",
      url: '../../../Treats/Edit',
      data: { 'treatId': $(`#TreatId-${id}`).val(), 'name': $(`#TreatName-${id}`).val(), 'description': $(`#treatDescription-${id}`).val(), 'countryOfOrigin': $(`#treatCountry-${id}`).val() },
      success: function () {
        $(`#cardDescription-${id}`).text($(`#treatDescription-${id}`).val());
        $(`#cardTitle-${id}`).text($(`#treatName-${id}`).val());
        $(`#cardCountry-${id}`).text($(`#treatCountry-${id}`).val());
        $(`#editModal-${id}`).modal('hide');
      },
      error: function () {
        alert(`Error while updating ${$("#bookTitleInput").val()}`);
      }
    });
  });
}
