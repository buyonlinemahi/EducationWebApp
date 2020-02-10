// We generated a client token for you so you can test out this code
// immediately. In a production-ready integration, you will need to
// generate a client token on your server (see section below).

var clientToken = $('#HFShippingPayment').val();

var form = document.querySelector('#cardForm');
var submit = document.querySelector('#btnplaceorder');

 

braintree.client.create({
    authorization: clientToken
}, function (err, clientInstance) {
    if (err) {
        console.error(err);
        return;
    }

    braintree.hostedFields.create({
        client: clientInstance,
        styles: {
            'input': {
                'font-size': '16px',
                'font-family': '"open_sans", verdana, sans-serif',
                'font-weight': 'lighter',
                'color': 'black'
            },
            ':focus': {
                'color': 'black'
            },
            '.valid': {
                'color': 'black'
            },
            '.invalid': {
                'color': 'red'
            }
        },
        fields: {
            number: {
                selector: '#card-number',
                placeholder: '1111 1111 1111 1111'
            },
            cvv: {
                selector: '#cvv',
                placeholder: '111'
            },
            expirationDate: {
                selector: '#expiration-date',
                placeholder: 'MM/YY'
            },
            postalCode: {
                selector: '#postal-code',
                placeholder: '11111' 

            }
        }
    }, function (err, hostedFieldsInstance) {
        if (err) {
            //console.error(err);
            alertify.alert(err);
            return;
        }

        hostedFieldsInstance.on('focus', function (event) {
            var field = event.fields[event.emittedBy];

            $(field.container).next('.hosted-field--label').addClass('label-float').removeClass('filled');
        });

        // Emulates floating label pattern
        hostedFieldsInstance.on('blur', function (event) {
            var field = event.fields[event.emittedBy];

            if (field.isEmpty) {
                $(field.container).next('.hosted-field--label').removeClass('label-float');
            } else if (event.isValid) {
                $(field.container).next('.hosted-field--label').addClass('filled');
            } else {
                $(field.container).next('.hosted-field--label').addClass('invalid');
            }
        });

        hostedFieldsInstance.on('empty', function (event) {
            var field = event.fields[event.emittedBy];
            $(field.container).next('.hosted-field--label').removeClass('filled').removeClass('invalid');
        });

        hostedFieldsInstance.on('cardTypeChange', function (event) {
            if (event.cards.length === 1) {
                $('#card-image').removeClass().addClass(event.cards[0].type);
                // Change the CVV length for AmericanExpress cards
                if (event.cards[0].code.size === 4) {
                    hostedFieldsInstance.setPlaceholder('cvv', '1234');
                }
            } else {
                hostedFieldsInstance.setPlaceholder('cvv', '123');
            }
        });

        hostedFieldsInstance.on('validityChange', function (event) {
            var field = event.fields[event.emittedBy];

            if (field.isPotentiallyValid) {
                $(field.container).next('.hosted-field--label').removeClass('invalid');
            } else {
                $(field.container).next('.hosted-field--label').addClass('invalid');
            }
        });
        submit.addEventListener('click', function (event) {
           
            $("#loaderDiv").addClass('loaderShippingPaymentbg');
            $("#loading").show();

            event.preventDefault();

            hostedFieldsInstance.tokenize(function (err, payload) {
                if (err) {
                    $("#loaderDiv").removeClass('loaderShippingPaymentbg');
                    $("#loading").hide();
                    alertify.alert(err.message);
                    return;
                }
                /// stop the timmer on shipping payment page.
                clearInterval(timer);
                document.getElementById('countdown').innerHTML = 'Payment Session Expired : 0' + minutes + ':' + seconds;

                
                
                document.querySelector('input[name="payment-method-nonce"]').value = payload.nonce;
                form.submit();
            });
        });
    });
});

$(document).ready(function () {
    $('a[data-toggle="tooltip"]').tooltip({
        animated: 'fade',
        placement: 'top',
        html: true
    });

    $(".postalCode").mask("99999?-9999")
                      .blur(function () {
                          var lastFour = $(this).val().substr(6, 4);
                          if (lastFour != "") {
                              if (lastFour.length != 4) {
                                  $(this).val("");
                              }
                          }
                      });
    
}); 
  




 


