function s3_swf_init(e,t){var n=void 0!=t.buttonWidth?t.buttonWidth:50,o=void 0!=t.buttonHeight?t.buttonHeight:50,i=void 0!=t.flashVersion?t.flashVersion:"9.0.0",a=void 0!=t.queueSizeLimit?t.queueSizeLimit:10,r=void 0!=t.fileSizeLimit?t.fileSizeLimit:524288e3,l=void 0!=t.fileTypes?t.fileTypes:"*.*",s=void 0!=t.fileTypeDescs?t.fileTypeDescs:"All Files",u=void 0!=t.selectMultipleFiles?t.selectMultipleFiles:!0,d=void 0!=t.keyPrefix?t.keyPrefix:"",c=void 0!=t.signaturePath?t.signaturePath:"s3_uploads.xml",f="/assets/s3_upload-b39254301f994c4cd0b4849ca9b4e5e3.swf",p="/assets/s3_up_button-1ba6b3be688995c4f58fbede1d121165.gif",v="/assets/s3_over_button-fc63125c64ccc48645dfa6f27c09cd15.gif",g="/assets/s3_down_button-b50ae429f4fdca783af2891f6acbdaa4.gif",h=void 0!=t.onFileAdd?t.onFileAdd:function(){},y=void 0!=t.onFileRemove?t.onFileRemove:function(){},m=void 0!=t.onFileSizeLimitReached?t.onFileSizeLimitReached:function(){},w=void 0!=t.onFileNotInQueue?t.onFileNotInQueue:function(){},b=void 0!=t.onQueueChange?t.onQueueChange:function(){},S=void 0!=t.onQueueClear?t.onQueueClear:function(){},E=void 0!=t.onQueueSizeLimitReached?t.onQueueSizeLimitReached:function(){},C=void 0!=t.onQueueEmpty?t.onQueueEmpty:function(){},F=void 0!=t.onUploadingStop?t.onUploadingStop:function(){},U=void 0!=t.onUploadingStart?t.onUploadingStart:function(){},I=void 0!=t.onUploadingFinish?t.onUploadingFinish:function(){},L=void 0!=t.onSignatureOpen?t.onSignatureOpen:function(){},T=void 0!=t.onSignatureProgress?t.onSignatureProgress:function(){},A=void 0!=t.onSignatureHttpStatus?t.onSignatureHttpStatus:function(){},N=void 0!=t.onSignatureComplete?t.onSignatureComplete:function(){},O=void 0!=t.onSignatureSecurityError?t.onSignatureSecurityError:function(){},j=void 0!=t.onSignatureIOError?t.onSignatureIOError:function(){},k=void 0!=t.onSignatureXMLError?t.onSignatureXMLError:function(){},Q=void 0!=t.onUploadOpen?t.onUploadOpen:function(){},_=void 0!=t.onUploadProgress?t.onUploadProgress:function(){},B=void 0!=t.onUploadHttpStatus?t.onUploadHttpStatus:function(){},P=void 0!=t.onUploadComplete?t.onUploadComplete:function(){},x=void 0!=t.onUploadIOError?t.onUploadIOError:function(){},M=void 0!=t.onUploadSecurityError?t.onUploadSecurityError:function(){},R=void 0!=t.onUploadError?t.onUploadError:function(){},V={s3_swf_obj:void 0!=t.swfVarObj?t.swfVarObj:"s3_swf"},$={},z={};$.wmode="transparent",$.menu="false",$.quality="low",s3_upload_swfobject.embedSWF(f+"?t="+(new Date).getTime(),e,n,o,i,!1,V,$,z);var H=window.location.protocol+"//"+window.location.host+c,D=window.location.protocol+"//"+window.location.host+p,W=window.location.protocol+"//"+window.location.host+g,X=window.location.protocol+"//"+window.location.host+v;return s3_swf={obj:function(){return document[e]},init:function(){this.obj().init(H,d,r,a,l,s,u,n,o,D,W,X)},clearQueue:function(){this.obj().clearQueue()},startUploading:function(){this.obj().startUploading()},stopUploading:function(){this.obj().stopUploading()},removeFileFromQueue:function(e){this.obj().removeFileFromQueue(e)},onFileAdd:h,onFileRemove:y,onFileSizeLimitReached:m,onFileNotInQueue:w,onQueueChange:b,onQueueClear:S,onQueueSizeLimitReached:E,onQueueEmpty:C,onUploadingStop:F,onUploadingStart:U,onUploadingFinish:I,onSignatureOpen:L,onSignatureProgress:T,onSignatureHttpStatus:A,onSignatureComplete:N,onSignatureSecurityError:O,onSignatureIOError:j,onSignatureXMLError:k,onUploadOpen:Q,onUploadProgress:_,onUploadHttpStatus:B,onUploadComplete:P,onUploadIOError:x,onUploadSecurityError:M,onUploadError:R}}var s3_upload_swfobject=function(){function e(){if(!z){try{var e=B.getElementsByTagName("body")[0].appendChild(h("span"));e.parentNode.removeChild(e)}catch(t){return}z=!0;for(var n=M.length,o=0;n>o;o++)M[o]()}}function t(e){z?e():M[M.length]=e}function n(e){if(typeof _.addEventListener!=T)_.addEventListener("load",e,!1);else if(typeof B.addEventListener!=T)B.addEventListener("load",e,!1);else if(typeof _.attachEvent!=T)y(_,"onload",e);else if("function"==typeof _.onload){var t=_.onload;_.onload=function(){t(),e()}}else _.onload=e}function o(){x?i():a()}function i(){var e=B.getElementsByTagName("body")[0],t=h(A);t.setAttribute("type",j);var n=e.appendChild(t);if(n){var o=0;!function(){if(typeof n.GetVariable!=T){var i=n.GetVariable("$version");i&&(i=i.split(" ")[1].split(","),W.pv=[parseInt(i[0],10),parseInt(i[1],10),parseInt(i[2],10)])}else if(10>o)return o++,setTimeout(arguments.callee,10),void 0;e.removeChild(t),n=null,a()}()}else a()}function a(){var e=R.length;if(e>0)for(var t=0;e>t;t++){var n=R[t].id,o=R[t].callbackFn,i={success:!1,id:n};if(W.pv[0]>0){var a=g(n);if(a)if(!m(R[t].swfVersion)||W.wk&&W.wk<312)if(R[t].expressInstall&&l()){var d={};d.data=R[t].expressInstall,d.width=a.getAttribute("width")||"0",d.height=a.getAttribute("height")||"0",a.getAttribute("class")&&(d.styleclass=a.getAttribute("class")),a.getAttribute("align")&&(d.align=a.getAttribute("align"));for(var c={},f=a.getElementsByTagName("param"),p=f.length,v=0;p>v;v++)"movie"!=f[v].getAttribute("name").toLowerCase()&&(c[f[v].getAttribute("name")]=f[v].getAttribute("value"));s(d,c,n,o)}else u(a),o&&o(i);else b(n,!0),o&&(i.success=!0,i.ref=r(n),o(i))}else if(b(n,!0),o){var h=r(n);h&&typeof h.SetVariable!=T&&(i.success=!0,i.ref=h),o(i)}}}function r(e){var t=null,n=g(e);if(n&&"OBJECT"==n.nodeName)if(typeof n.SetVariable!=T)t=n;else{var o=n.getElementsByTagName(A)[0];o&&(t=o)}return t}function l(){return!H&&m("6.0.65")&&(W.win||W.mac)&&!(W.wk&&W.wk<312)}function s(e,t,n,o){H=!0,F=o||null,U={success:!1,id:n};var i=g(n);if(i){"OBJECT"==i.nodeName?(E=d(i),C=null):(E=i,C=n),e.id=k,(typeof e.width==T||!/%$/.test(e.width)&&parseInt(e.width,10)<310)&&(e.width="310"),(typeof e.height==T||!/%$/.test(e.height)&&parseInt(e.height,10)<137)&&(e.height="137"),B.title=B.title.slice(0,47)+" - Flash Player Installation";var a=W.ie&&W.win?"ActiveX":"PlugIn",r="MMredirectURL="+_.location.toString().replace(/&/g,"%26")+"&MMplayerType="+a+"&MMdoctitle="+B.title;if(typeof t.flashvars!=T?t.flashvars+="&"+r:t.flashvars=r,W.ie&&W.win&&4!=i.readyState){var l=h("div");n+="SWFObjectNew",l.setAttribute("id",n),i.parentNode.insertBefore(l,i),i.style.display="none",function(){4==i.readyState?i.parentNode.removeChild(i):setTimeout(arguments.callee,10)}()}c(e,t,n)}}function u(e){if(W.ie&&W.win&&4!=e.readyState){var t=h("div");e.parentNode.insertBefore(t,e),t.parentNode.replaceChild(d(e),t),e.style.display="none",function(){4==e.readyState?e.parentNode.removeChild(e):setTimeout(arguments.callee,10)}()}else e.parentNode.replaceChild(d(e),e)}function d(e){var t=h("div");if(W.win&&W.ie)t.innerHTML=e.innerHTML;else{var n=e.getElementsByTagName(A)[0];if(n){var o=n.childNodes;if(o)for(var i=o.length,a=0;i>a;a++)1==o[a].nodeType&&"PARAM"==o[a].nodeName||8==o[a].nodeType||t.appendChild(o[a].cloneNode(!0))}}return t}function c(e,t,n){var o,i=g(n);if(W.wk&&W.wk<312)return o;if(i)if(typeof e.id==T&&(e.id=n),W.ie&&W.win){var a="";for(var r in e)e[r]!=Object.prototype[r]&&("data"==r.toLowerCase()?t.movie=e[r]:"styleclass"==r.toLowerCase()?a+=' class="'+e[r]+'"':"classid"!=r.toLowerCase()&&(a+=" "+r+'="'+e[r]+'"'));var l="";for(var s in t)t[s]!=Object.prototype[s]&&(l+='<param name="'+s+'" value="'+t[s]+'" />');i.outerHTML='<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"'+a+">"+l+"</object>",V[V.length]=e.id,o=g(e.id)}else{var u=h(A);u.setAttribute("type",j);for(var d in e)e[d]!=Object.prototype[d]&&("styleclass"==d.toLowerCase()?u.setAttribute("class",e[d]):"classid"!=d.toLowerCase()&&u.setAttribute(d,e[d]));for(var c in t)t[c]!=Object.prototype[c]&&"movie"!=c.toLowerCase()&&f(u,c,t[c]);i.parentNode.replaceChild(u,i),o=u}return o}function f(e,t,n){var o=h("param");o.setAttribute("name",t),o.setAttribute("value",n),e.appendChild(o)}function p(e){var t=g(e);t&&"OBJECT"==t.nodeName&&(W.ie&&W.win?(t.style.display="none",function(){4==t.readyState?v(e):setTimeout(arguments.callee,10)}()):t.parentNode.removeChild(t))}function v(e){var t=g(e);if(t){for(var n in t)"function"==typeof t[n]&&(t[n]=null);t.parentNode.removeChild(t)}}function g(e){var t=null;try{t=B.getElementById(e)}catch(n){}return t}function h(e){return B.createElement(e)}function y(e,t,n){e.attachEvent(t,n),$[$.length]=[e,t,n]}function m(e){var t=W.pv,n=e.split(".");return n[0]=parseInt(n[0],10),n[1]=parseInt(n[1],10)||0,n[2]=parseInt(n[2],10)||0,t[0]>n[0]||t[0]==n[0]&&t[1]>n[1]||t[0]==n[0]&&t[1]==n[1]&&t[2]>=n[2]?!0:!1}function w(e,t,n,o){if(!W.ie||!W.mac){var i=B.getElementsByTagName("head")[0];if(i){var a=n&&"string"==typeof n?n:"screen";if(o&&(I=null,L=null),!I||L!=a){var r=h("style");r.setAttribute("type","text/css"),r.setAttribute("media",a),I=i.appendChild(r),W.ie&&W.win&&typeof B.styleSheets!=T&&B.styleSheets.length>0&&(I=B.styleSheets[B.styleSheets.length-1]),L=a}W.ie&&W.win?I&&typeof I.addRule==A&&I.addRule(e,t):I&&typeof B.createTextNode!=T&&I.appendChild(B.createTextNode(e+" {"+t+"}"))}}}function b(e,t){if(D){var n=t?"visible":"hidden";z&&g(e)?g(e).style.visibility=n:w("#"+e,"visibility:"+n)}}function S(e){var t=/[\\\"<>\.;]/,n=null!=t.exec(e);return n&&typeof encodeURIComponent!=T?encodeURIComponent(e):e}var E,C,F,U,I,L,T="undefined",A="object",N="Shockwave Flash",O="ShockwaveFlash.ShockwaveFlash",j="application/x-shockwave-flash",k="SWFObjectExprInst",Q="onreadystatechange",_=window,B=document,P=navigator,x=!1,M=[o],R=[],V=[],$=[],z=!1,H=!1,D=!0,W=function(){var e=typeof B.getElementById!=T&&typeof B.getElementsByTagName!=T&&typeof B.createElement!=T,t=P.userAgent.toLowerCase(),n=P.platform.toLowerCase(),o=n?/win/.test(n):/win/.test(t),i=n?/mac/.test(n):/mac/.test(t),a=/webkit/.test(t)?parseFloat(t.replace(/^.*webkit\/(\d+(\.\d+)?).*$/,"$1")):!1,r=!1,l=[0,0,0],s=null;if(typeof P.plugins!=T&&typeof P.plugins[N]==A)s=P.plugins[N].description,!s||typeof P.mimeTypes!=T&&P.mimeTypes[j]&&!P.mimeTypes[j].enabledPlugin||(x=!0,r=!1,s=s.replace(/^.*\s+(\S+\s+\S+$)/,"$1"),l[0]=parseInt(s.replace(/^(.*)\..*$/,"$1"),10),l[1]=parseInt(s.replace(/^.*\.(.*)\s.*$/,"$1"),10),l[2]=/[a-zA-Z]/.test(s)?parseInt(s.replace(/^.*[a-zA-Z]+(.*)$/,"$1"),10):0);else if(typeof _.ActiveXObject!=T)try{var u=new ActiveXObject(O);u&&(s=u.GetVariable("$version"),s&&(r=!0,s=s.split(" ")[1].split(","),l=[parseInt(s[0],10),parseInt(s[1],10),parseInt(s[2],10)]))}catch(d){}return{w3:e,pv:l,wk:a,ie:r,win:o,mac:i}}();return function(){W.w3&&((typeof B.readyState!=T&&"complete"==B.readyState||typeof B.readyState==T&&(B.getElementsByTagName("body")[0]||B.body))&&e(),z||(typeof B.addEventListener!=T&&B.addEventListener("DOMContentLoaded",e,!1),W.ie&&W.win&&(B.attachEvent(Q,function(){"complete"==B.readyState&&(B.detachEvent(Q,arguments.callee),e())}),_==top&&function(){if(!z){try{B.documentElement.doScroll("left")}catch(t){return setTimeout(arguments.callee,0),void 0}e()}}()),W.wk&&function(){return z?void 0:/loaded|complete/.test(B.readyState)?(e(),void 0):(setTimeout(arguments.callee,0),void 0)}(),n(e)))}(),function(){W.ie&&W.win&&window.attachEvent("onunload",function(){for(var e=$.length,t=0;e>t;t++)$[t][0].detachEvent($[t][1],$[t][2]);for(var n=V.length,o=0;n>o;o++)p(V[o]);for(var i in W)W[i]=null;W=null;for(var a in swfobject)swfobject[a]=null;swfobject=null})}(),{registerObject:function(e,t,n,o){if(W.w3&&e&&t){var i={};i.id=e,i.swfVersion=t,i.expressInstall=n,i.callbackFn=o,R[R.length]=i,b(e,!1)}else o&&o({success:!1,id:e})},getObjectById:function(e){return W.w3?r(e):void 0},embedSWF:function(e,n,o,i,a,r,u,d,f,p){var v={success:!1,id:n};W.w3&&!(W.wk&&W.wk<312)&&e&&n&&o&&i&&a?(b(n,!1),t(function(){o+="",i+="";var t={};if(f&&typeof f===A)for(var g in f)t[g]=f[g];t.data=e,t.width=o,t.height=i;var h={};if(d&&typeof d===A)for(var y in d)h[y]=d[y];if(u&&typeof u===A)for(var w in u)typeof h.flashvars!=T?h.flashvars+="&"+w+"="+u[w]:h.flashvars=w+"="+u[w];if(m(a)){var S=c(t,h,n);t.id==n&&b(n,!0),v.success=!0,v.ref=S}else{if(r&&l())return t.data=r,s(t,h,n,p),void 0;b(n,!0)}p&&p(v)})):p&&p(v)},switchOffAutoHideShow:function(){D=!1},ua:W,getFlashPlayerVersion:function(){return{major:W.pv[0],minor:W.pv[1],release:W.pv[2]}},hasFlashPlayerVersion:m,createSWF:function(e,t,n){return W.w3?c(e,t,n):void 0},showExpressInstall:function(e,t,n,o){W.w3&&l()&&s(e,t,n,o)},removeSWF:function(e){W.w3&&p(e)},createCSS:function(e,t,n,o){W.w3&&w(e,t,n,o)},addDomLoadEvent:t,addLoadEvent:n,getQueryParamValue:function(e){var t=B.location.search||B.location.hash;if(t){if(/\?/.test(t)&&(t=t.split("?")[1]),null==e)return S(t);for(var n=t.split("&"),o=0;o<n.length;o++)if(n[o].substring(0,n[o].indexOf("="))==e)return S(n[o].substring(n[o].indexOf("=")+1))}return""},expressInstallCallback:function(){if(H){var e=g(k);e&&E&&(e.parentNode.replaceChild(E,e),C&&(b(C,!0),W.ie&&W.win&&(E.style.display="block")),F&&F(U)),H=!1}}}}(),s3_swf;