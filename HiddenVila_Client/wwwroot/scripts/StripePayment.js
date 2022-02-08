redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51KQUGJD5gcJvCRHZEzE3bXUD1mWzadDJVwE8G4PJipB92AP8hnHK95I8gRO93DQtnbKxHAwwTu2PflKNLDtGXUKZ00EztcfDlP');
    stripe.redirectToCheckout({ 
        sessionId: sessionId
    });
};