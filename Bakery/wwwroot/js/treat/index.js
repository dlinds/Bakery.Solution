function CreateClickHandlersForEditDelete(id) {
  $(`form#editForm-${id}`).submit(function (event) {
    event.preventDefault();
    $.ajax({
      type: "POST",
      url: '../../../Treats/Edit',
      data: { 'treatId': $(`#treatId-${id}`).val(), 'name': $(`#treatName-${id}`).val(), 'description': $(`#treatDescription-${id}`).val(), 'countryOfOrigin': $(`#treatCountry-${id}`).val() },
      success: function () {
        $(`#cardDescription-${id}`).text($(`#treatDescription-${id}`).val());
        $(`#cardTitle-${id}`).text($(`#treatName-${id}`).val());
        $(`#cardCountry-${id}`).text($(`#treatCountry-${id}`).val());
        $(`#editModal-${id}`).modal('hide');
      },
      error: function (response) {
        if (response.status == 401) {
          alert("Hello! It looks like you aren't yet logged in. Only authenticated users may add, edit, or delete treats.");
        } else {
          alert(`We got a ${response.status} error while editing the treat! Sorry about that, but something has gone wrong.`);
        }
      }
    });
  });

  $(`#delete-${id}`).click(function () {
    $.ajax({
      type: "POST",
      url: '../../../Treats/Delete',
      data: { 'treatId': $(`#treatId-${id}`).val() },
      success: function () {
        $(`#treatCard-${id}`).remove();
        $(`#deleteModal-${id}`).modal('hide');
        $(`#editModal-${id}`).modal('hide');
      },
      error: function (response) {
        if (response.status == 401) {
          alert("Hello! It looks like you aren't yet logged in. Only authenticated users may add, edit, or delete treats.");
        } else {
          alert(`We got a ${response.status} error while deleting the treat! Sorry about that, but something has gone wrong.`);
        }
      }
    });
  });
}


// function AddClickHandlerForAddTreat() {
//   $(`form#addTreat`).submit(function (event) {
//     event.preventDefault();
//     $.ajax({
//       type: "POST",
//       url: '../../../Treats/Create',
//       data: { 'name': $(`#addTreatName`).val(), 'description': $(`#addTreatDescription`).val(), 'countryOfOrigin': $(`#addTreatCountry`).val() },
//       success: function () {
//         location.reload();
//       },
//       error: function (response) {
//         if (response.status == 401) {
//           alert("Hello! It looks like you aren't yet logged in. Only authenticated users may add, edit, or delete treats.");
//         } else {
//           alert(`We got a ${response.status} error while adding the treat! Sorry about that, but something has gone wrong.`);
//         }
//       }
//     });
//   });
// }
