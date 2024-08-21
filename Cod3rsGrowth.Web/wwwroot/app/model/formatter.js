sap.ui.define([], () => {
    "use strict";

    return {
        formatarCpf: function(sCpf) {
            if(sCpf != undefined){
                return sCpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
            }
            return sCpf;
        },
        formatarCnpj: function(sCnpj){
            if(sCnpj != undefined){
                return sCnpj.replace(/^(\d{2})(\d{3})?(\d{3})?(\d{4})?(\d{2})?/, "$1.$2.$3/$4-$5");
            }
            return sCnpj;
        },
        formatarInputCpf: async function(sCpf) {
            if(sCpf != undefined){
                return sCpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
            }
            return sCpf;
        },
        formatarInputCnpj: async function(sCnpj){
            if(sCnpj != undefined){
                return sCnpj.replace(/^(\d{2})(\d{3})?(\d{3})?(\d{4})?(\d{2})?/, "$1.$2.$3/$4-$5");
            }
            return sCnpj;
        },
        formatarData: function(sData) {
            if(sData != undefined) {
                return sData.slice(0, 10);
            }
            return sData;
        },
        formatarCartao: function(sCartao) {
            if(sCartao != undefined) {
                return sCartao.replace(/(\d{4})(\d{4})(\d{4})(\d{4})/, '$1 $2 $3 $4');
            }
            return sCartao;
        }
    };
});
