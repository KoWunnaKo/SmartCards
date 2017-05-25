var erros = {
'01':  'Ошибка получения соединения к ридеру',//'SCardEstablishContext call error',
'02' : 'Ошибка получения списка ридеров',//'SCardListReaders get cards list error.',
'03' : 'Не полный список ридеров',//'Invalid Readers List error',
'04' : 'Ошибка подключения к Карте',//'SCardConnect Connect to card error.',
'05' : 'Ошибка при выборе апплета на карте',//'Select Applet Error',
'06' : 'Ошибка получения уникального идентификатора пользователя',//'Get Unique User Id error',
'07' : 'Ошибка ввода неправильного ПИНа пользователя или блокировка карты в следствии неправильных попыток',//'Invalid user login data due to User PIN being locked due to too many bad retries or simply wrong PIN code',
'08' : 'Неизвестная ошибка при логировании пользователя',//'User Login unknown error. See Log file',
'09':  'Ошибка ввода неправильного ПИНа администратора или блокировка карты в следствии неправильных попыток',//'Invalid admin login data due to User PIN being locked due to too many bad retries or simply wrong PIN code',
'10':  'Неизвестная ошибка при логировании администратора',//'Admin Login unknown error. See Log file',
'11' : 'Неверная длина ПИНа пользователя. ПИН должен состоять из синволов от 4 до 16 байт',//'Wrong length of PIN. PIN must be 4 to 16 bytes long (userChangePin)',
'12' : 'Для выполнения команды изменения ПИНа требуется логирование пользователя',//'Login is required for this command to execute (userChangePin)',
'13' : 'Неизвестная ошибка при изменении ПИНа пользователя',//'User Change Pin unknown error.',
'14':  'Неверная длина ПИНа администратора. ПИН должен состоять из синволов от 4 до 16 байт',//'Wrong length of PIN. PIN must be 4 to 16 bytes long (adminChangePuk)',
'15' : 'Логирование администратора требуется для осуществления операции изменения ПИНа администратора',//'Login is required for this command to execute (adminChangePuk)',
'16':  'Неизвестная ошибка при изменении ПИНа администратора',//'Admin Change Puk unknown error.',
'17' : 'Для осуществление операции сброса ПИНа юзера требуется логирование администратора',//'Login is required for this command to execute (adminResetUserPin)',
'18' : 'Неизвестная ошибка при сбросе ПИНа пользователя',//'adminResetUserPin unknown error.',
'19' : 'Невозможно найти открытый ключ на карте. Карта требует реинициализации',//'Public Key Modulus cannot be found due to possible fault in the card. Reinstallation of card applet is necessary',
'20' : 'Неизвестная ошибка при считовании открытого ключа',//'Read Public Key Modulus unknown error.',
'21' : 'Для осуществление команды подпись Токена требуется требуется логирование пользователя',//'Login is required for this command to execute (signToken)',
'23' : 'Неизвестная ошибка при подписи Токена',//'signToken unknown error.',
'24' : 'Для осуществление операции загрузки сертификата требуется логирование Администратора',//'Admin login is required for this command to execute (uploadCA)',
'25' : 'Неправильная длина сертификата',//'Wrong length of data have been supplied (uploadCertificate)',
'26' : 'Внутренная ошибка загрузки сертификата. Требуется реинициализация апплета ',//'An internal error in the ‘Upload Certificate’ state has been detected. Re-installing the card applet is required (uploadCertificate)',
'27' : 'Неизвестная ошибка загрузки сертификата',//'uploadCertificate unknown error.',
'28' : 'Процесс загрузки сертификата неудачно завершено. Попробуйте заново',//'A CA Signed Certificate upload process have not been completed successfully and thus the certificate size could not be determined. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate size.',
'29':  'Процесс загрузки сертификата неудачно завершено. Попробуйте заново',//'A CA Signed Certificate upload process have not been completed successfully and thus the certificate data could not be safely read. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate data fragments.',
'30' : 'Неправильный диапозон считывание сертификата',//'An invalid certificate read range have been issued',
'31':  'Неправильный диапозон считывание сертификата',//'An invalid certificate read range with negative reading values have been issued',
'32' : 'Неизвестная ошибка считования сертификата',//'Read certificate unknown error',
'33' : 'Неверный формат обращение',//'Invalid Content Data length (Protokol error)',
'34':  'Неверный входящий ТАГ',//'Invalid EntryTag',
'35' : 'Пользователь отклонил ввод ПИНа',//'User Denied PIN Enter',
'36' : 'Общая ошибака. Попробуйте заново'//'Unknown Error'
};


var CAPIWS = {
    URL: (window.location.protocol.toLowerCase() === "https:" ? "wss://127.0.0.1:5585" : "ws://127.0.0.1:5585") + "/smartcard",    
    callFunction: function(funcDef, callback, error){
        if (!window.WebSocket){
            if(error)
                error();
            return;
        }
        var socket;
        try{
            socket = new WebSocket(this.URL);
        } catch (e) {
            alert(e.toString());
            error(e);
        }
        socket.onerror = function (e) {
            alert(e.message);

            if(error)
                error(e);
        };
        socket.onmessage = function(event){
            var data = JSON.parse(event.data);
            socket.close();
            callback(event,data);
        };
        socket.onopen = function(){   
            socket.send(JSON.stringify(funcDef));
        };
    }
};

var wsError = function (e) {    
    if (e) {
        uiShowMessage(errorCAPIWS + " : " + e);
    } else {
        uiShowMessage(errorBrowserWS);
    }
};

var SmartCardLib = {

    certificate : function() {
        
                    CAPIWS.callFunction({funcName: "getCertificate"}, function (event, data) {
                    if (data.status) {



                        if (data.certificate.length === 2) {

                            var desc = erros[data.certificate];

                            return {certificate: '', errorCode: data.certificate, errorDesc : desc};
                        }
                        else
                        {
                            return {certificate: data.certificate, errorCode: 0, errorDesc : ''};
                        }
                    } else {
                        wsError('Unknown Error GEt Certificate Failed');
                    }
                }, function (e) {
                    wsError('Unknown Error GEt Certificate Failed');
                });
    },


    putToken : function name(token) {
        
            CAPIWS.callFunction({funcName: "putToken", inputParm:token }, function (event, data) {
                    if (data.status) {

                        if (data.signToken.length === 2)
                        {

                            var desc = erros[data.certificate];

                            return {signToken: '', errorCode: data.signToken, errorDesc : desc};    

                        }
                        else
                        {
                            return {signToken: data.signToken, errorCode: 0, errorDesc : ''}; 
                        }
                    } else {
                        alert('Failed');
                    }
                }, function (e) {
                    alert('Failed Totally');
                });
    }
};


