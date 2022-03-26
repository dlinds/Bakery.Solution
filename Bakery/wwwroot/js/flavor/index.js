function CreateClickHandlersForEditDelete(id, flavorName) {
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
        $("#toastBody").text(`Your edit of ${$(`#flavorName-${id}`).val()} was successful`);
        $("#toastMessage").text("Edited!");
        $("#successfulToastAlert").toast("show");
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
        $(`#deleteModal-${id}`).modal('hide');
        $(`#flavorCard-${id}`).remove();
        $("#toastMessage").text("Deleted!");
        $("#toastBody").text(`You successfully deleted ${flavorName}`);
        $("#successfulToastAlert").toast("show");
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

function AddClickHandlerForTreatToggles(flavorId, treatId, flavorName, treatName) {
  $(`#treat-${treatId}-flavor-${flavorId}`).change(function () {
    if (this.checked) {
      $.ajax({
        type: "POST",
        url: '../../../Treats/AddFlavor',
        data: { 'treatId': treatId, 'flavorId': flavorId },
        success: function () {
          $("#toastMessage").text("Added!");
          $("#toastBody").text(`${flavorName} was successfully added to ${treatName}`);
          $("#successfulToastAlert").toast("show");
        },
        error: function (response) {
          if (response.status == 401) {
            alert("Hello! It looks like you aren't yet logged in. Only authenticated users may add, edit, or delete treats.");
          } else {
            alert(`We got a ${response.status} error while deleting the treat! Sorry about that, but something has gone wrong.`);
          }
        }
      });
    } else {
      $.ajax({
        type: "POST",
        url: '../../../Treats/RemoveFlavor',
        data: { 'treatId': treatId, 'flavorId': flavorId },
        success: function () {
          $("#toastMessage").text("Removed!");
          $("#toastBody").text(`${flavorName} was successfully removed from ${treatName}`);
          $("#successfulToastAlert").toast("show");
        },
        error: function (response) {
          if (response.status == 401) {
            alert("Hello! It looks like you aren't yet logged in. Only authenticated users may add, edit, or delete treats.");
          } else {
            alert(`We got a ${response.status} error while deleting the treat! Sorry about that, but something has gone wrong.`);
          }
        }
      });
    }
  });
}
