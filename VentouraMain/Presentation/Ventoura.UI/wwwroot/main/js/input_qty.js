// Quantity buttons TOURS
	function qtySum_tours(){
    var arr = document.getElementsByName('qtyInput_tours');
    var tot=0;
    for(var i=0;i<arr.length;i++){
        if(parseInt(arr[i].value))
            tot += parseInt(arr[i].value);
    }

    var cardQty = document.querySelector(".qtyTotal.tours");
    cardQty.innerHTML = tot;
	} 
	qtySum_tours();

	$(function() {

	   $(".qtyButtons.tours input").after('<div class="qtyInc tours"></div>');
	   $(".qtyButtons.tours input").before('<div class="qtyDec tours"></div>');
	   $(".qtyDec.tours, .qtyInc.tours").on("click", function() {

		  var $button = $(this);
		  var oldValue = $button.parent().find("input").val();

		  if ($button.hasClass('qtyInc tours')) {
			 var newVal = parseFloat(oldValue) + 1;
		  } else {
			 // don't allow decrementing below zero
			 if (oldValue > 0) {
				var newVal = parseFloat(oldValue) - 1;
			 } else {
				newVal = 0;
			 }
		  }

		  $button.parent().find("input").val(newVal);
		  qtySum_tours();
		  $(".qtyTotal.tours").addClass("rotate-x");

	   });

	   function removeAnimation() { $(".qtyTotal.tours").removeClass("rotate-x"); }
	   const counter = document.querySelector(".qtyTotal.tours");
	   counter.addEventListener("animationend", removeAnimation);

	});

// Quantity buttons HOTELS
	function qtySum_hotels(){
    var arr = document.getElementsByName('qtyInput_hotels');
    var tot=0;
    for(var i=0;i<arr.length;i++){
        if(parseInt(arr[i].value))
            tot += parseInt(arr[i].value);
    }

    var cardQty = document.querySelector(".qtyTotal.hotels");
    cardQty.innerHTML = tot;
	} 
	qtySum_hotels();

	$(function() {

	   $(".qtyButtons.hotels input").after('<div class="qtyInc hotels"></div>');
	   $(".qtyButtons.hotels input").before('<div class="qtyDec hotels"></div>');
	   $(".qtyDec.hotels, .qtyInc.hotels").on("click", function() {

		  var $button = $(this);
		  var oldValue = $button.parent().find("input").val();

		  if ($button.hasClass('qtyInc hotels')) {
			 var newVal = parseFloat(oldValue) + 1;
		  } else {
			 // don't allow decrementing below zero
			 if (oldValue > 0) {
				var newVal = parseFloat(oldValue) - 1;
			 } else {
				newVal = 0;
			 }
		  }

		  $button.parent().find("input").val(newVal);
		  qtySum_hotels();
		  $(".qtyTotal.hotels").addClass("rotate-x");

	   });

	   function removeAnimation() { $(".qtyTotal.hotels").removeClass("rotate-x"); }
	   const counter = document.querySelector(".qtyTotal.hotels");
	   counter.addEventListener("animationend", removeAnimation);

	});	

	// Quantity buttons RESTAURANT
	function qtySum_restaurants(){
    var arr = document.getElementsByName('qtyInput_restaurants');
    var tot=0;
    for(var i=0;i<arr.length;i++){
        if(parseInt(arr[i].value))
            tot += parseInt(arr[i].value);
    }

    var cardQty = document.querySelector(".qtyTotal.restaurants");
    cardQty.innerHTML = tot;
	} 
	qtySum_restaurants();

	$(function() {

	   $(".qtyButtons.restaurants input").after('<div class="qtyInc restaurants"></div>');
	   $(".qtyButtons.restaurants input").before('<div class="qtyDec restaurants"></div>');
	   $(".qtyDec.restaurants, .qtyInc.restaurants").on("click", function() {

		  var $button = $(this);
		  var oldValue = $button.parent().find("input").val();

		  if ($button.hasClass('qtyInc restaurants')) {
			 var newVal = parseFloat(oldValue) + 1;
		  } else {
			 // don't allow decrementing below zero
			 if (oldValue > 0) {
				var newVal = parseFloat(oldValue) - 1;
			 } else {
				newVal = 0;
			 }
		  }

		  $button.parent().find("input").val(newVal);
		  qtySum_restaurants();
		  $(".qtyTotal.restaurants").addClass("rotate-x");

	   });

	   function removeAnimation() { $(".qtyTotal.restaurants").removeClass("rotate-x"); }
	   const counter = document.querySelector(".qtyTotal.restaurants");
	   counter.addEventListener("animationend", removeAnimation);

	});	