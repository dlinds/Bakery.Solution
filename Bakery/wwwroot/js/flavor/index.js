function CreateClickHandlersForEditDelete(id) {
  $(`form#editForm-${id}`).submit(function (event) {
    event.preventDefault();
    $.ajax({
      type: "POST",
      url: '../../../Flavors/Edit',
      data: { 'flavorId': $(`#flavorId-${id}`).val(), 'name': $(`#flavorName-${id}`).val(), 'spiceLevel': $(`#flavorSpiceLevel-${id}`).val() },
      success: function () {
        $(`#cardTitle-${id}`).text($(`#flavorName-${id}`).val());
        $(`#cardSpiceLevel-${id}`).text($(`#flavorSpiceLevel-${id}`).val());
        $(`#editModal-${id}`).modal('hide');
      },
      error: function (response) {
        if (response.status == 401) {
          alert("Hello! It looks like you aren't yet logged in. Only authenticated users may add, edit, or delete flavors.");
        } else {
          alert(`We got a ${response.status} error while editing the flavor! Sorry about that, but something has gone wrong.`);
        }
      }
    });
  });

  $(`#delete-${id}`).click(function () {
    $.ajax({
      type: "POST",
      url: '../../../Flavors/Delete',
      data: { 'flavorId': $(`#flavorId-${id}`).val() },
      success: function () {
        $(`#flavorCard-${id}`).remove();
        $(`#deleteModal-${id}`).modal('hide');
      },
      error: function (response) {
        if (response.status == 401) {
          alert("Hello! It looks like you aren't yet logged in. Only authenticated users may add, edit, or delete flavors.");
        } else {
          alert(`We got a ${response.status} error while deleting the flavor! Sorry about that, but something has gone wrong.`);
        }
      }
    });
  });
}
