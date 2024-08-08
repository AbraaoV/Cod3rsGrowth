sap.ui.define([], () => {
    "use strict";

    return {
        formatarCpf: function(sCpf) {
            if(sCpf != undefined){
                return sCpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
            }
            return sCpf
        },
		formatarCnpj: function(sCnpj){
            if(sCnpj != undefined){
                return sCnpj.replace(/^(\d{2})(\d{3})?(\d{3})?(\d{4})?(\d{2})?/, "$1.$2.$3/$4-$5");
            }
            return sCnpj
		}
    };
});
