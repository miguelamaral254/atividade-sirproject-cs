const AppUtils = {
    applyPhoneMask: function(input) {
        let value = input.value.replace(/\D/g, '');
        if (value.length > 11) value = value.slice(0, 11);
        
        let formatted = '';
        if (value.length > 0) {
            formatted += '(' + value.substring(0, 2);
            if (value.length > 2) {
                formatted += ') ' + value.substring(2, 3);
                if (value.length > 3) {
                    formatted += ' ' + value.substring(3, 7);
                    if (value.length > 7) {
                        formatted += '-' + value.substring(7, 11);
                    }
                }
            }
        }
        input.value = formatted;
    }
};
