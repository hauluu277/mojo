(function (g, j, c, f, d, k, i) {
    /*! Jssor */
    new (function () {
    }
    );
    var e = {
        zb: function (a) {
            return a
        },
        Yb: function (a) {
            return -a * (a - 2)
        },
        Be: function (a) {
            return (a *= 2) < 1 ? 1 / 2 * a * a : -1 / 2 * (--a * (a - 2) - 1)
        },
        Ff: function (a) {
            return a == 0 || a == 1 ? a : (a *= 2) < 1 ? 1 / 2 * c.pow(2, 10 * (a - 1)) : 1 / 2 * (-c.pow(2, -10 * --a) + 2)
        },
        wd: function (a) {
            return 1 - c.cos(a * c.PI * 2)
        },
        Te: function (a) {
            return 1 - ((a *= 2) < 1 ? (a = 1 - a) * a * a : (a -= 1) * a * a)
        }
    };
    var b = new function () {
        var h = this, zb = /\S+/g, H = 1, bb = 2, eb = 3, db = 4, hb = 5, I, s = 0, l = 0, t = 0, Z = 0, B = 0, K = navigator, mb = K.appName, o = K.userAgent, p = parseFloat;
        function Ib() {
            if (!I) {
                I = {
                    lf: "ontouchstart" in g || "createTouch" in j
                };
                var a;
                if (K.pointerEnabled || (a = K.msPointerEnabled))
                    I.Rc = a ? "msTouchAction" : "touchAction"
            }
            return I
        }
        function w(h) {
            if (!s) {
                s = -1;
                if (mb == "Microsoft Internet Explorer" && !!g.attachEvent && !!g.ActiveXObject) {
                    var e = o.indexOf("MSIE");
                    s = H;
                    t = p(o.substring(e + 5, o.indexOf(";", e)));
                    /*@cc_on Z=@_jscript_version@*/
                    ; l = j.documentMode || t
                } else if (mb == "Netscape" && !!g.addEventListener) {
                    var d = o.indexOf("Firefox")
                      , b = o.indexOf("Safari")
                      , f = o.indexOf("Chrome")
                      , c = o.indexOf("AppleWebKit");
                    if (d >= 0) {
                        s = bb;
                        l = p(o.substring(d + 8))
                    } else if (b >= 0) {
                        var i = o.substring(0, b).lastIndexOf("/");
                        s = f >= 0 ? db : eb;
                        l = p(o.substring(i + 1, b))
                    } else {
                        var a = /Trident\/.*rv:([0-9]{1,}[\.0-9]{0,})/i.exec(o);
                        if (a) {
                            s = H;
                            l = t = p(a[1])
                        }
                    }
                    if (c >= 0)
                        B = p(o.substring(c + 12))
                } else {
                    var a = /(opera)(?:.*version|)[ \/]([\w.]+)/i.exec(o);
                    if (a) {
                        s = hb;
                        l = p(a[2])
                    }
                }
            }
            return h == s
        }
        function q() {
            return w(H)
        }
        function xb() {
            return q() && (l < 6 || j.compatMode == "BackCompat")
        }
        function Ab() {
            return w(bb)
        }
        function cb() {
            return w(eb)
        }
        function gb() {
            return w(hb)
        }
        function tb() {
            return cb() && B > 534 && B < 535
        }
        function L() {
            w();
            return B > 537 || l > 42 || s == H && l >= 11
        }
        function vb() {
            return q() && l < 9
        }
        function ub(a) {
            var b, c;
            return function (f) {
                if (!b) {
                    b = d;
                    var e = a.substr(0, 1).toUpperCase() + a.substr(1);
                    n([a].concat(["WebKit", "ms", "Moz", "O", "webkit"]), function (g, d) {
                        var b = a;
                        if (d)
                            b = g + e;
                        if (f.style[b] != i)
                            return c = b
                    })
                }
                return c
            }
        }
        function sb(b) {
            var a;
            return function (c) {
                a = a || ub(b)(c) || b;
                return a
            }
        }
        var M = sb("transform");
        function lb(a) {
            return {}.toString.call(a)
        }
        var ib = {};
        n(["Boolean", "Number", "String", "Function", "Array", "Date", "RegExp", "Object"], function (a) {
            ib["[object " + a + "]"] = a.toLowerCase()
        });
        function n(b, d) {
            var a, c;
            if (lb(b) == "[object Array]") {
                for (a = 0; a < b.length; a++)
                    if (c = d(b[a], a, b))
                        return c
            } else
                for (a in b)
                    if (c = d(b[a], a, b))
                        return c
        }
        function E(a) {
            return a == f ? String(a) : ib[lb(a)] || "object"
        }
        function jb(a) {
            for (var b in a)
                return d
        }
        function C(a) {
            try {
                return E(a) == "object" && !a.nodeType && a != a.window && (!a.constructor || {}.hasOwnProperty.call(a.constructor.prototype, "isPrototypeOf"))
            } catch (b) { }
        }
        function v(a, b) {
            return {
                x: a,
                y: b
            }
        }
        function pb(b, a) {
            setTimeout(b, a || 0)
        }
        function J(b, d, c) {
            var a = !b || b == "inherit" ? "" : b;
            n(d, function (c) {
                var b = c.exec(a);
                if (b) {
                    var d = a.substr(0, b.index)
                      , e = a.substr(b.index + b[0].length + 1, a.length - 1);
                    a = d + e
                }
            });
            a && (c += (!a.indexOf(" ") ? "" : " ") + a);
            return c
        }
        function rb(b, a) {
            if (l < 9)
                b.style.filter = a
        }
        function Hb(a, b) {
            if (a === i)
                a = b;
            return a
        }
        h.mf = Ib;
        h.Tc = q;
        h.nf = xb;
        h.of = Ab;
        h.pf = cb;
        h.qf = L;
        ub("transform");
        h.Id = function () {
            return l
        }
        ;
        h.Qd = function () {
            return t || l
        }
        ;
        h.rf = function () {
            w();
            return B
        }
        ;
        h.L = pb;
        h.sf = function (a, b) {
            b.call(a);
            return D({}, a)
        }
        ;
        function W(a) {
            a.constructor === W.caller && a.Sb && a.Sb.apply(a, W.caller.arguments)
        }
        h.Sb = W;
        h.Gb = function (a) {
            if (h.af(a))
                a = j.getElementById(a);
            return a
        }
        ;
        function u(a) {
            return a || g.event
        }
        h.Ec = function (b) {
            b = u(b);
            var a = b.target || b.srcElement || j;
            if (a.nodeType == 3)
                a = h.Ac(a);
            return a
        }
        ;
        h.rd = function (a) {
            a = u(a);
            return {
                x: a.pageX || a.clientX || 0,
                y: a.pageY || a.clientY || 0
            }
        }
        ;
        function x(c, d, a) {
            if (a !== i)
                c.style[d] = a == i ? "" : a;
            else {
                var b = c.currentStyle || c.style;
                a = b[d];
                if (a == "" && g.getComputedStyle) {
                    b = c.ownerDocument.defaultView.getComputedStyle(c, f);
                    b && (a = b.getPropertyValue(d) || b[d])
                }
                return a
            }
        }
        function Y(b, c, a, d) {
            if (a === i) {
                a = p(x(b, c));
                isNaN(a) && (a = f);
                return a
            }
            if (a == f)
                a = "";
            else
                d && (a += "px");
            x(b, c, a)
        }
        function m(c, a) {
            var d = a ? Y : x, b;
            if (a & 4)
                b = sb(c);
            return function (e, f) {
                return d(e, b ? b(e) : c, f, a & 2)
            }
        }
        function Cb(b) {
            if (q() && t < 9) {
                var a = /opacity=([^)]*)/.exec(b.style.filter || "");
                return a ? p(a[1]) / 100 : 1
            } else
                return p(b.style.opacity || "1")
        }
        function Eb(b, a, f) {
            if (q() && t < 9) {
                var h = b.style.filter || ""
                  , i = new RegExp(/[\s]*alpha\([^\)]*\)/g)
                  , e = c.round(100 * a)
                  , d = "";
                if (e < 100 || f)
                    d = "alpha(opacity=" + e + ") ";
                var g = J(h, [i], d);
                rb(b, g)
            } else
                b.style.opacity = a == 1 ? "" : c.round(a * 100) / 100
        }
        var N = {
            C: ["rotate"],
            W: ["rotateX"],
            X: ["rotateY"],
            Vb: ["skewX"],
            Rb: ["skewY"]
        };
        if (!L())
            N = D(N, {
                D: ["scaleX", 2],
                E: ["scaleY", 2],
                ab: ["translateZ", 1]
            });
        function O(d, a) {
            var c = "";
            if (a) {
                if (q() && l && l < 10) {
                    delete a.W;
                    delete a.X;
                    delete a.ab
                }
                b.i(a, function (d, b) {
                    var a = N[b];
                    if (a) {
                        var e = a[1] || 0;
                        if (P[b] != d)
                            c += " " + a[0] + "(" + d + (["deg", "px", ""])[e] + ")"
                    }
                });
                if (L()) {
                    if (a.pb || a.ob || a.ab != i)
                        c += " translate3d(" + (a.pb || 0) + "px," + (a.ob || 0) + "px," + (a.ab || 0) + "px)";
                    if (a.D == i)
                        a.D = 1;
                    if (a.E == i)
                        a.E = 1;
                    if (a.D != 1 || a.E != 1)
                        c += " scale3d(" + a.D + ", " + a.E + ", 1)"
                }
            }
            d.style[M(d)] = c
        }
        h.Ke = m("transformOrigin", 4);
        h.Le = m("backfaceVisibility", 4);
        h.Me = m("transformStyle", 4);
        h.Ne = m("perspective", 6);
        h.Oe = m("perspectiveOrigin", 4);
        h.Pe = function (b, a) {
            if (q() && t < 9 || t < 10 && xb())
                b.style.zoom = a == 1 ? "" : a;
            else {
                var c = M(b)
                  , f = a == 1 ? "" : "scale(" + a + ")"
                  , e = b.style[c]
                  , g = new RegExp(/[\s]*scale\(.*?\)/g)
                  , d = J(e, [g], f);
                b.style[c] = d
            }
        }
        ;
        h.c = function (a, d, b, c) {
            a = h.Gb(a);
            if (a.addEventListener) {
                d == "mousewheel" && a.addEventListener("DOMMouseScroll", b, c);
                a.addEventListener(d, b, c)
            } else if (a.attachEvent) {
                a.attachEvent("on" + d, b);
                c && a.setCapture && a.setCapture()
            }
        }
        ;
        h.Q = function (a, c, d, b) {
            a = h.Gb(a);
            if (a.removeEventListener) {
                c == "mousewheel" && a.removeEventListener("DOMMouseScroll", d, b);
                a.removeEventListener(c, d, b)
            } else if (a.detachEvent) {
                a.detachEvent("on" + c, d);
                b && a.releaseCapture && a.releaseCapture()
            }
        }
        ;
        h.Qb = function (a) {
            a = u(a);
            a.preventDefault && a.preventDefault();
            a.cancel = d;
            a.returnValue = k
        }
        ;
        h.Se = function (a) {
            a = u(a);
            a.stopPropagation && a.stopPropagation();
            a.cancelBubble = d
        }
        ;
        h.M = function (d, c) {
            var a = [].slice.call(arguments, 2)
              , b = function () {
                  var b = a.concat([].slice.call(arguments, 0));
                  return c.apply(d, b)
              };
            return b
        }
        ;
        h.vf = function (a, b) {
            if (b == i)
                return a.textContent || a.innerText;
            var c = j.createTextNode(b);
            h.fc(a);
            a.appendChild(c)
        }
        ;
        h.Nb = function (d, c) {
            for (var b = [], a = d.firstChild; a; a = a.nextSibling)
                (c || a.nodeType == 1) && b.push(a);
            return b
        }
        ;
        function kb(a, c, e, b) {
            b = b || "u";
            for (a = a ? a.firstChild : f; a; a = a.nextSibling)
                if (a.nodeType == 1) {
                    if (U(a, b) == c)
                        return a;
                    if (!e) {
                        var d = kb(a, c, e, b);
                        if (d)
                            return d
                    }
                }
        }
        h.pc = kb;
        function T(a, d, g, b) {
            b = b || "u";
            var c = [];
            for (a = a ? a.firstChild : f; a; a = a.nextSibling)
                if (a.nodeType == 1) {
                    U(a, b) == d && c.push(a);
                    if (!g) {
                        var e = T(a, d, g, b);
                        if (e.length)
                            c = c.concat(e)
                    }
                }
            return c
        }
        function fb(a, c, d) {
            for (a = a ? a.firstChild : f; a; a = a.nextSibling)
                if (a.nodeType == 1) {
                    if (a.tagName == c)
                        return a;
                    if (!d) {
                        var b = fb(a, c, d);
                        if (b)
                            return b
                    }
                }
        }
        h.Ue = fb;
        h.We = function (b, a) {
            return b.getElementsByTagName(a)
        }
        ;
        h.Ab = function (a, f, d) {
            d = d || "u";
            var e;
            do {
                if (a.nodeType == 1) {
                    var c = b.n(a, d);
                    if (c && c == Hb(f, c)) {
                        e = a;
                        break
                    }
                }
                a = b.Ac(a)
            } while (a && a != j.body); return e
        }
        ;
        function D() {
            var e = arguments, d, c, b, a, g = 1 & e[0], f = 1 + g;
            d = e[f - 1] || {};
            for (; f < e.length; f++)
                if (c = e[f])
                    for (b in c) {
                        a = c[b];
                        if (a !== i) {
                            a = c[b];
                            var h = d[b];
                            d[b] = g && (C(h) || C(a)) ? D(g, {}, h, a) : a
                        }
                    }
            return d
        }
        h.J = D;
        function X(f, g) {
            var d = {}, c, a, b;
            for (c in f) {
                a = f[c];
                b = g[c];
                if (a !== b) {
                    var e;
                    if (C(a) && C(b)) {
                        a = X(a, b);
                        e = !jb(a)
                    }
                    !e && (d[c] = a)
                }
            }
            return d
        }
        h.Zc = function (a) {
            return E(a) == "function"
        }
        ;
        h.af = function (a) {
            return E(a) == "string"
        }
        ;
        h.Nc = function (a) {
            return !isNaN(p(a)) && isFinite(a)
        }
        ;
        h.i = n;
        h.fe = C;
        function R(a) {
            return j.createElement(a)
        }
        h.oc = function () {
            return R("DIV")
        }
        ;
        h.wf = function () {
            return R("SPAN")
        }
        ;
        h.Uc = function () { }
        ;
        function y(b, c, a) {
            if (a == i)
                return b.getAttribute(c);
            b.setAttribute(c, a)
        }
        function U(a, b) {
            return y(a, b) || y(a, "data-" + b)
        }
        h.G = y;
        h.n = U;
        h.Ub = function (d, b, c) {
            var a = h.Yc(y(d, b));
            if (isNaN(a))
                a = c;
            return a
        }
        ;
        function z(b, a) {
            return y(b, "class", a) || ""
        }
        function ob(b) {
            var a = {};
            n(b, function (b) {
                if (b != i)
                    a[b] = b
            });
            return a
        }
        function qb(b, a) {
            return b.match(a || zb)
        }
        function Q(b, a) {
            return ob(qb(b || "", a))
        }
        h.Sf = ob;
        h.Rf = qb;
        function ab(b, c) {
            var a = "";
            n(c, function (c) {
                a && (a += b);
                a += c
            });
            return a
        }
        function F(a, c, b) {
            z(a, ab(" ", D(X(Q(z(a)), Q(c)), Q(b))))
        }
        h.Ac = function (a) {
            return a.parentNode
        }
        ;
        h.P = function (a) {
            h.tb(a, "none")
        }
        ;
        h.H = function (a, b) {
            h.tb(a, b ? "none" : "")
        }
        ;
        h.Of = function (b, a) {
            b.removeAttribute(a)
        }
        ;
        h.Mf = function () {
            return q() && l < 10
        }
        ;
        h.Df = function (d, a) {
            if (a)
                d.style.clip = "rect(" + c.round(a.f || a.v || 0) + "px " + c.round(a.m) + "px " + c.round(a.l) + "px " + c.round(a.g || a.u || 0) + "px)";
            else if (a !== i) {
                var g = d.style.cssText
                  , f = [new RegExp(/[\s]*clip: rect\(.*?\)[;]?/i), new RegExp(/[\s]*cliptop: .*?[;]?/i), new RegExp(/[\s]*clipright: .*?[;]?/i), new RegExp(/[\s]*clipbottom: .*?[;]?/i), new RegExp(/[\s]*clipleft: .*?[;]?/i)]
                  , e = J(g, f, "");
                b.id(d, e)
            }
        }
        ;
        h.hb = function () {
            return +new Date
        }
        ;
        h.N = function (b, a) {
            b.appendChild(a)
        }
        ;
        h.Cb = function (b, a, c) {
            (c || a.parentNode).insertBefore(b, a)
        }
        ;
        h.ec = function (b, a) {
            a = a || b.parentNode;
            a && a.removeChild(b)
        }
        ;
        h.Lf = function (a, b) {
            n(a, function (a) {
                h.ec(a, b)
            })
        }
        ;
        h.fc = function (a) {
            h.Lf(h.Nb(a, d), a)
        }
        ;
        h.fd = function (a, b) {
            var c = h.Ac(a);
            b & 1 && h.O(a, (h.s(c) - h.s(a)) / 2);
            b & 2 && h.U(a, (h.z(c) - h.z(a)) / 2)
        }
        ;
        var S = {
            f: f,
            m: f,
            l: f,
            g: f,
            A: f,
            q: f
        };
        h.If = function (a) {
            var b = h.oc();
            r(b, {
                gc: "block",
                ub: h.K(a),
                f: 0,
                g: 0,
                A: 0,
                q: 0
            });
            var d = h.bd(a, S);
            h.Cb(b, a);
            h.N(b, a);
            var e = h.bd(a, S)
              , c = {};
            n(d, function (b, a) {
                if (b == e[a])
                    c[a] = b
            });
            r(b, S);
            r(b, c);
            r(a, {
                f: 0,
                g: 0
            });
            return c
        }
        ;
        h.Yc = p;
        function V(d, c, b) {
            var a = d.cloneNode(!c);
            !b && h.Of(a, "id");
            return a
        }
        h.ib = V;
        h.Hb = function (e, f) {
            var a = new Image;
            function b(e, d) {
                h.Q(a, "load", b);
                h.Q(a, "abort", c);
                h.Q(a, "error", c);
                f && f(a, d)
            }
            function c(a) {
                b(a, d)
            }
            if (gb() && l < 11.6 || !e)
                b(!e);
            else {
                h.c(a, "load", b);
                h.c(a, "abort", c);
                h.c(a, "error", c);
                a.src = e
            }
        }
        ;
        h.Jf = function (d, a, e) {
            var c = d.length + 1;
            function b(b) {
                c--;
                if (a && b && b.src == a.src)
                    a = b;
                !c && e && e(a)
            }
            n(d, function (a) {
                h.Hb(a.src, b)
            });
            b()
        }
        ;
        h.Kf = function (a, g, i, h) {
            if (h)
                a = V(a);
            var c = T(a, g);
            if (!c.length)
                c = b.We(a, g);
            for (var f = c.length - 1; f > -1; f--) {
                var d = c[f]
                  , e = V(i);
                z(e, z(d));
                b.id(e, d.style.cssText);
                b.Cb(e, d);
                b.ec(d)
            }
            return a
        }
        ;
        function Fb(a) {
            var l = this, p = "", r = ["av", "pv", "ds", "dn"], d = [], q, k = 0, f = 0, e = 0;
            function g() {
                F(a, q, (d[e || f & 2 || f] || "") + " " + (d[k] || ""));
                b.rb(a, "pointer-events", e ? "none" : "")
            }
            function c() {
                k = 0;
                g();
                h.Q(j, "mouseup", c);
                h.Q(j, "touchend", c);
                h.Q(j, "touchcancel", c)
            }
            function o(a) {
                if (e)
                    h.Qb(a);
                else {
                    k = 4;
                    g();
                    h.c(j, "mouseup", c);
                    h.c(j, "touchend", c);
                    h.c(j, "touchcancel", c)
                }
            }
            l.Fe = function (a) {
                if (a === i)
                    return f;
                f = a & 2 || a & 1;
                g()
            }
            ;
            l.bc = function (a) {
                if (a === i)
                    return !e;
                e = a ? 0 : 3;
                g()
            }
            ;
            l.gb = a = h.Gb(a);
            y(a, "data-jssor-button", "1");
            var m = b.Rf(z(a));
            if (m)
                p = m.shift();
            n(r, function (a) {
                d.push(p + a)
            });
            q = ab(" ", d);
            d.unshift("");
            h.c(a, "mousedown", o);
            h.c(a, "touchstart", o)
        }
        h.zc = function (a) {
            return new Fb(a)
        }
        ;
        h.rb = x;
        h.cc = m("overflow");
        h.U = m("top", 2);
        h.Zd = m("right", 2);
        h.be = m("bottom", 2);
        h.O = m("left", 2);
        h.s = m("width", 2);
        h.z = m("height", 2);
        h.de = m("marginLeft", 2);
        h.Td = m("marginTop", 2);
        h.K = m("position");
        h.tb = m("display");
        h.F = m("zIndex", 1);
        h.jc = function (b, a, c) {
            if (a != i)
                Eb(b, a, c);
            else
                return Cb(b)
        }
        ;
        h.id = function (a, b) {
            if (b != i)
                a.style.cssText = b;
            else
                return a.style.cssText
        }
        ;
        h.Xd = function (b, a) {
            if (a === i) {
                a = x(b, "backgroundImage") || "";
                var c = /\burl\s*\(\s*["']?([^"'\r\n,]+)["']?\s*\)/gi.exec(a) || [];
                return c[1]
            }
            x(b, "backgroundImage", a ? "url('" + a + "')" : "")
        }
        ;
        var G;
        h.Yd = G = {
            I: h.jc,
            f: h.U,
            m: h.Zd,
            l: h.be,
            g: h.O,
            A: h.s,
            q: h.z,
            ub: h.K,
            gc: h.tb,
            T: h.F
        };
        h.bd = function (c, b) {
            var a = {};
            n(b, function (d, b) {
                if (G[b])
                    a[b] = G[b](c)
            });
            return a
        }
        ;
        function r(g, l) {
            var e = vb()
              , b = L()
              , d = tb()
              , j = M(g);
            function k(b, d, a) {
                var e = b.qb(v(-d / 2, -a / 2))
                  , f = b.qb(v(d / 2, -a / 2))
                  , g = b.qb(v(d / 2, a / 2))
                  , h = b.qb(v(-d / 2, a / 2));
                b.qb(v(300, 300));
                return v(c.min(e.x, f.x, g.x, h.x) + d / 2, c.min(e.y, f.y, g.y, h.y) + a / 2)
            }
            function a(d, a) {
                a = a || {};
                var n = a.ab || 0
                  , p = (a.W || 0) % 360
                  , q = (a.X || 0) % 360
                  , u = (a.C || 0) % 360
                  , l = a.D
                  , m = a.E
                  , f = a.Zf;
                if (l == i)
                    l = 1;
                if (m == i)
                    m = 1;
                if (f == i)
                    f = 1;
                if (e) {
                    n = 0;
                    p = 0;
                    q = 0;
                    f = 0
                }
                var c = new Bb(a.pb, a.ob, n);
                c.W(p);
                c.X(q);
                c.Wd(u);
                c.Vd(a.Vb, a.Rb);
                c.Eb(l, m, f);
                if (b) {
                    c.vb(a.u, a.v);
                    d.style[j] = c.ie()
                } else if (!Z || Z < 9) {
                    var o = ""
                      , g = {
                          x: 0,
                          y: 0
                      };
                    if (a.fb)
                        g = k(c, a.fb, a.kb);
                    h.Td(d, g.y);
                    h.de(d, g.x);
                    o = c.ze();
                    var s = d.style.filter
                      , t = new RegExp(/[\s]*progid:DXImageTransform\.Microsoft\.Matrix\([^\)]*\)/g)
                      , r = J(s, [t], o);
                    rb(d, r)
                }
            }
            r = function (e, c) {
                c = c || {};
                var j = c.u, k = c.v, g;
                n(G, function (a, b) {
                    g = c[b];
                    g !== i && a(e, g)
                });
                h.Df(e, c.a);
                if (!b) {
                    j != i && h.O(e, (c.jd || 0) + j);
                    k != i && h.U(e, (c.kd || 0) + k)
                }
                if (c.ye)
                    if (d)
                        pb(h.M(f, O, e, c));
                    else
                        a(e, c)
            }
            ;
            h.ac = O;
            if (d)
                h.ac = r;
            if (e)
                h.ac = a;
            else if (!b)
                a = O;
            h.B = r;
            r(g, l)
        }
        h.ac = r;
        h.B = r;
        function Bb(j, k, o) {
            var d = this
              , b = [1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, j || 0, k || 0, o || 0, 1]
              , i = c.sin
              , h = c.cos
              , l = c.tan;
            function g(a) {
                return a * c.PI / 180
            }
            function n(a, b) {
                return {
                    x: a,
                    y: b
                }
            }
            function m(c, e, l, m, o, r, t, u, w, z, A, C, E, b, f, k, a, g, i, n, p, q, s, v, x, y, B, D, F, d, h, j) {
                return [c * a + e * p + l * x + m * F, c * g + e * q + l * y + m * d, c * i + e * s + l * B + m * h, c * n + e * v + l * D + m * j, o * a + r * p + t * x + u * F, o * g + r * q + t * y + u * d, o * i + r * s + t * B + u * h, o * n + r * v + t * D + u * j, w * a + z * p + A * x + C * F, w * g + z * q + A * y + C * d, w * i + z * s + A * B + C * h, w * n + z * v + A * D + C * j, E * a + b * p + f * x + k * F, E * g + b * q + f * y + k * d, E * i + b * s + f * B + k * h, E * n + b * v + f * D + k * j]
            }
            function e(c, a) {
                return m.apply(f, (a || b).concat(c))
            }
            d.Eb = function (a, c, d) {
                if (a != 1 || c != 1 || d != 1)
                    b = e([a, 0, 0, 0, 0, c, 0, 0, 0, 0, d, 0, 0, 0, 0, 1])
            }
            ;
            d.vb = function (a, c, d) {
                b[12] += a || 0;
                b[13] += c || 0;
                b[14] += d || 0
            }
            ;
            d.W = function (c) {
                if (c) {
                    a = g(c);
                    var d = h(a)
                      , f = i(a);
                    b = e([1, 0, 0, 0, 0, d, f, 0, 0, -f, d, 0, 0, 0, 0, 1])
                }
            }
            ;
            d.X = function (c) {
                if (c) {
                    a = g(c);
                    var d = h(a)
                      , f = i(a);
                    b = e([d, 0, -f, 0, 0, 1, 0, 0, f, 0, d, 0, 0, 0, 0, 1])
                }
            }
            ;
            d.Wd = function (c) {
                if (c) {
                    a = g(c);
                    var d = h(a)
                      , f = i(a);
                    b = e([d, f, 0, 0, -f, d, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1])
                }
            }
            ;
            d.Vd = function (a, c) {
                if (a || c) {
                    j = g(a);
                    k = g(c);
                    b = e([1, l(k), 0, 0, l(j), 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1])
                }
            }
            ;
            d.qb = function (c) {
                var a = e(b, [1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, c.x, c.y, 0, 1]);
                return n(a[12], a[13])
            }
            ;
            d.ie = function () {
                return "matrix3d(" + b.join(",") + ")"
            }
            ;
            d.ze = function () {
                return "progid:DXImageTransform.Microsoft.Matrix(M11=" + b[0] + ", M12=" + b[4] + ", M21=" + b[1] + ", M22=" + b[5] + ", SizingMethod='auto expand')"
            }
        }
        new function () {
            var a = this;
            function b(d, g) {
                for (var j = d[0].length, i = d.length, h = g[0].length, f = [], c = 0; c < i; c++)
                    for (var k = f[c] = [], b = 0; b < h; b++) {
                        for (var e = 0, a = 0; a < j; a++)
                            e += d[c][a] * g[a][b];
                        k[b] = e
                    }
                return f
            }
            a.D = function (b, c) {
                return a.md(b, c, 0)
            }
            ;
            a.E = function (b, c) {
                return a.md(b, 0, c)
            }
            ;
            a.md = function (a, c, d) {
                return b(a, [[c, 0], [0, d]])
            }
            ;
            a.qb = function (d, c) {
                var a = b(d, [[c.x], [c.y]]);
                return v(a[0][0], a[1][0])
            }
        }
        ;
        var P = {
            jd: 0,
            kd: 0,
            u: 0,
            v: 0,
            V: 1,
            D: 1,
            E: 1,
            C: 0,
            W: 0,
            X: 0,
            pb: 0,
            ob: 0,
            ab: 0,
            Vb: 0,
            Rb: 0
        };
        h.nd = function (c, d) {
            var a = c || {};
            if (c)
                if (b.Zc(c))
                    a = {
                        db: a
                    };
                else if (b.Zc(c.a))
                    a.a = {
                        db: c.a
                    };
            a.db = a.db || d;
            if (a.a)
                a.a.db = a.a.db || d;
            return a
        }
        ;
        h.Pc = function (n, j, s, t, B, C, o) {
            var a = j;
            if (n) {
                a = {};
                for (var h in j) {
                    var D = C[h] || 1
                      , z = B[h] || [0, 1]
                      , g = (s - z[0]) / z[1];
                    g = c.min(c.max(g, 0), 1);
                    g = g * D;
                    var x = c.floor(g);
                    if (g != x)
                        g -= x;
                    var k = t.db || e.zb, m, E = n[h], p = j[h];
                    if (b.Nc(p)) {
                        k = t[h] || k;
                        var A = k(g);
                        m = E + p * A
                    } else {
                        m = b.J({
                            Tb: {}
                        }, n[h]);
                        var y = t[h] || {};
                        b.i(p.Tb || p, function (d, a) {
                            k = y[a] || y.db || k;
                            var c = k(g)
                              , b = d * c;
                            m.Tb[a] = b;
                            m[a] += b
                        })
                    }
                    a[h] = m
                }
                var w = b.i(j, function (b, a) {
                    return P[a] != i
                });
                w && b.i(P, function (c, b) {
                    if (a[b] == i && n[b] !== i)
                        a[b] = n[b]
                });
                if (w) {
                    if (a.V)
                        a.D = a.E = a.V;
                    a.fb = o.fb;
                    a.kb = o.kb;
                    if (q() && l >= 11 && (j.u || j.v) && s != 0 && s != 1)
                        a.C = a.C || 1e-8;
                    a.ye = d
                }
            }
            if (j.a && o.vb) {
                var r = a.a.Tb
                  , v = (r.f || 0) + (r.l || 0)
                  , u = (r.g || 0) + (r.m || 0);
                a.g = (a.g || 0) + u;
                a.f = (a.f || 0) + v;
                a.a.g -= u;
                a.a.m -= u;
                a.a.f -= v;
                a.a.l -= v
            }
            if (a.a && b.Mf() && !a.a.f && !a.a.g && !a.a.v && !a.a.u && a.a.m == o.fb && a.a.l == o.kb)
                a.a = f;
            return a
        }
    }
    ;
    function o() {
        var a = this
          , d = [];
        function i(a, b) {
            d.push({
                tc: a,
                sc: b
            })
        }
        function h(a, c) {
            b.i(d, function (b, e) {
                b.tc == a && b.sc === c && d.splice(e, 1)
            })
        }
        a.Bb = a.addEventListener = i;
        a.removeEventListener = h;
        a.j = function (a) {
            var c = [].slice.call(arguments, 1);
            b.i(d, function (b) {
                b.tc == a && b.sc.apply(g, c)
            })
        }
    }
    var l = function (A, E, h, J, M, L) {
        A = A || 0;
        var a = this, p, m, n, s, C = 0, G, H, F, D, z = 0, i = 0, l = 0, y, j, e, f, o, x, u = [], w;
        function O(a) {
            e += a;
            f += a;
            j += a;
            i += a;
            l += a;
            z += a
        }
        function r(p) {
            var g = p;
            if (o)
                if (!x && (g >= f || g < e) || x && g >= e)
                    g = ((g - e) % o + o) % o + e;
            if (!y || s || i != g) {
                var k = c.min(g, f);
                k = c.max(k, e);
                if (!y || s || k != l) {
                    if (L) {
                        var m = (k - j) / (E || 1);
                        if (h.Kd)
                            m = 1 - m;
                        var n = b.Pc(M, L, m, G, F, H, h);
                        if (w)
                            b.i(n, function (b, a) {
                                w[a] && w[a](J, b)
                            });
                        else
                            b.B(J, n)
                    }
                    a.mc(l - j, k - j);
                    var r = l
                      , q = l = k;
                    b.i(u, function (b, c) {
                        var a = !y && x || g <= i ? u[u.length - c - 1] : b;
                        a.S(l - z)
                    });
                    i = g;
                    y = d;
                    a.Lb(r, q)
                }
            }
        }
        function B(a, b, d) {
            b && a.Ob(f);
            if (!d) {
                e = c.min(e, a.qd() + z);
                f = c.max(f, a.ic() + z)
            }
            u.push(a)
        }
        var v = g.requestAnimationFrame || g.webkitRequestAnimationFrame || g.mozRequestAnimationFrame || g.msRequestAnimationFrame;
        if (b.pf() && b.Id() < 7 || !v)
            v = function (a) {
                b.L(a, h.cb)
            }
        ;
        function I() {
            if (p) {
                var d = b.hb()
                  , e = c.min(d - C, h.vd)
                  , a = i + e * n;
                C = d;
                if (a * n >= m * n)
                    a = m;
                r(a);
                if (!s && a * n >= m * n)
                    K(D);
                else
                    v(I)
            }
        }
        function q(g, h, j) {
            if (!p) {
                p = d;
                s = j;
                D = h;
                g = c.max(g, e);
                g = c.min(g, f);
                m = g;
                n = m < i ? -1 : 1;
                a.ud();
                C = b.hb();
                v(I)
            }
        }
        function K(b) {
            if (p) {
                s = p = D = k;
                a.td();
                b && b()
            }
        }
        a.sd = function (a, b, c) {
            q(a ? i + a : f, b, c)
        }
        ;
        a.Hd = q;
        a.lb = K;
        a.ae = function (a) {
            q(a)
        }
        ;
        a.bb = function () {
            return i
        }
        ;
        a.Md = function () {
            return m
        }
        ;
        a.nb = function () {
            return l
        }
        ;
        a.S = r;
        a.vb = function (a) {
            r(i + a)
        }
        ;
        a.Od = function () {
            return p
        }
        ;
        a.le = function (a) {
            o = a
        }
        ;
        a.Ob = O;
        a.Ld = function (a, b) {
            B(a, 0, b)
        }
        ;
        a.Ic = function (a) {
            B(a, 1)
        }
        ;
        a.qd = function () {
            return e
        }
        ;
        a.ic = function () {
            return f
        }
        ;
        a.Lb = a.ud = a.td = a.mc = b.Uc;
        a.Cc = b.hb();
        h = b.J({
            cb: 16,
            vd: 50
        }, h);
        o = h.Bc;
        x = h.se;
        w = h.te;
        e = j = A;
        f = A + E;
        H = h.yb || {};
        F = h.Dc || {};
        G = b.nd(h.p)
    };
    var m = {
        Xb: "data-scale",
        Gc: "data-scale-ratio",
        Fb: "data-autocenter"
    }
      , n = new function () {
          var a = this;
          a.sb = function (c, a, e, d) {
              (d || !b.G(c, a)) && b.G(c, a, e)
          }
          ;
          a.Kc = function (a) {
              var c = b.Ub(a, m.Fb);
              b.fd(a, c)
          }
      }
      , p = new function () {
          var h = this
            , t = 1
            , q = 2
            , r = 4
            , s = 8
            , w = 256
            , x = 512
            , v = 1024
            , u = 2048
            , j = u + t
            , i = u + q
            , o = x + t
            , m = x + q
            , n = w + r
            , k = w + s
            , l = v + r
            , p = v + s;
          function y(a) {
              return (a & q) == q
          }
          function z(a) {
              return (a & r) == r
          }
          function g(b, a, c) {
              c.push(a);
              b[a] = b[a] || [];
              b[a].push(c)
          }
          h.cd = function (f) {
              for (var d = f.k, e = f.o, s = f.Ib, t = f.ad, r = [], a = 0, b = 0, p = d - 1, q = e - 1, h = t - 1, c, b = 0; b < e; b++)
                  for (a = 0; a < d; a++) {
                      switch (s) {
                          case j:
                              c = h - (a * e + (q - b));
                              break;
                          case l:
                              c = h - (b * d + (p - a));
                              break;
                          case o:
                              c = h - (a * e + b);
                          case n:
                              c = h - (b * d + a);
                              break;
                          case i:
                              c = a * e + b;
                              break;
                          case k:
                              c = b * d + (p - a);
                              break;
                          case m:
                              c = a * e + (q - b);
                              break;
                          default:
                              c = b * d + a
                      }
                      g(r, c, [b, a])
                  }
              return r
          }
          ;
          h.Sc = function (q) {
              var u = q.k
                , v = q.o
                , e = q.Ib
                , t = q.ad
                , r = []
                , s = 0
                , c = 0
                , d = 0
                , f = u - 1
                , h = v - 1
                , w = t - 1;
              switch (e) {
                  case j:
                  case m:
                  case o:
                  case i:
                      var a = 0
                        , b = 0;
                      break;
                  case k:
                  case l:
                  case n:
                  case p:
                      var a = f
                        , b = 0;
                      break;
                  default:
                      e = p;
                      var a = f
                        , b = 0
              }
              c = a;
              d = b;
              while (s < t) {
                  if (z(e) || y(e))
                      g(r, w - s++, [d, c]);
                  else
                      g(r, s++, [d, c]);
                  switch (e) {
                      case j:
                      case m:
                          c--;
                          d++;
                          break;
                      case o:
                      case i:
                          c++;
                          d--;
                          break;
                      case k:
                      case l:
                          c--;
                          d--;
                          break;
                      case p:
                      case n:
                      default:
                          c++;
                          d++
                  }
                  if (c < 0 || d < 0 || c > f || d > h) {
                      switch (e) {
                          case j:
                          case m:
                              a++;
                              break;
                          case k:
                          case l:
                          case o:
                          case i:
                              b++;
                              break;
                          case p:
                          case n:
                          default:
                              a--
                      }
                      if (a < 0 || b < 0 || a > f || b > h) {
                          switch (e) {
                              case j:
                              case m:
                                  a = f;
                                  b++;
                                  break;
                              case o:
                              case i:
                                  b = h;
                                  a++;
                                  break;
                              case k:
                              case l:
                                  b = h;
                                  a--;
                                  break;
                              case p:
                              case n:
                              default:
                                  a = 0;
                                  b++
                          }
                          if (b > h)
                              b = h;
                          else if (b < 0)
                              b = 0;
                          else if (a > f)
                              a = f;
                          else if (a < 0)
                              a = 0
                      }
                      d = b;
                      c = a
                  }
              }
              return r
          }
          ;
          h.Nf = function (d) {
              for (var e = [], a, b = 0; b < d.o; b++)
                  for (a = 0; a < d.k; a++)
                      g(e, c.ceil(1e5 * c.random()) % 13, [b, a]);
              return e
          }
      }
      , t = function (m, s, q, u, z, A) {
          var a = this, v, h, g, y = 0, x = u.xe, r, i = 8;
          function t(a) {
              if (a.f)
                  a.v = a.f;
              if (a.g)
                  a.u = a.g;
              b.i(a, function (a) {
                  b.fe(a) && t(a)
              })
          }
          function j(h, f, g) {
              var a = {
                  cb: f,
                  Y: 1,
                  L: 0,
                  k: 1,
                  o: 1,
                  I: 0,
                  V: 0,
                  a: 0,
                  vb: k,
                  Z: k,
                  Kd: k,
                  xb: p.Nf,
                  Ib: 1032,
                  Vc: {
                      me: 0,
                      qe: 0
                  },
                  p: e.zb,
                  yb: {},
                  Kb: [],
                  Dc: {}
              };
              b.J(a, h);
              if (a.o == 0)
                  a.o = c.round(a.k * g);
              t(a);
              a.ad = a.k * a.o;
              a.p = b.nd(a.p, e.zb);
              a.re = c.ceil(a.Y / a.cb);
              a.oe = function (c, b) {
                  c /= a.k;
                  b /= a.o;
                  var f = c + "x" + b;
                  if (!a.Kb[f]) {
                      a.Kb[f] = {
                          A: c,
                          q: b
                      };
                      for (var d = 0; d < a.k; d++)
                          for (var e = 0; e < a.o; e++)
                              a.Kb[f][e + "," + d] = {
                                  f: e * b,
                                  m: d * c + c,
                                  l: e * b + b,
                                  g: d * c
                              }
                  }
                  return a.Kb[f]
              }
              ;
              if (a.kc) {
                  a.kc = j(a.kc, f, g);
                  a.Z = d
              }
              return a
          }
          function n(z, i, a, v, n, l) {
              var y = this, t, u = {}, h = {}, m = [], f, e, r, p = a.Vc.me || 0, q = a.Vc.qe || 0, g = a.oe(n, l), o = B(a), C = o.length - 1, s = a.Y + a.L * C, w = v + s, j = a.Z, x;
              w += 50;
              function B(a) {
                  var b = a.xb(a);
                  return a.Kd ? b.reverse() : b
              }
              y.Dd = w;
              y.Mb = function (d) {
                  d -= v;
                  var e = d < s;
                  if (e || x) {
                      x = e;
                      if (!j)
                          d = s - d;
                      var f = c.ceil(d / a.cb);
                      b.i(h, function (a, e) {
                          var d = c.max(f, a.ne);
                          d = c.min(d, a.length - 1);
                          if (a.Rd != d) {
                              if (!a.Rd && !j)
                                  b.H(m[e]);
                              else
                                  d == a.je && j && b.P(m[e]);
                              a.Rd = d;
                              b.B(m[e], a[d])
                          }
                      })
                  }
              }
              ;
              i = b.ib(i);
              A(i, 0, 0);
              b.i(o, function (i, m) {
                  b.i(i, function (G) {
                      var I = G[0]
                        , H = G[1]
                        , v = I + "," + H
                        , o = k
                        , s = k
                        , x = k;
                      if (p && H % 2) {
                          if (p & 3)
                              o = !o;
                          if (p & 12)
                              s = !s;
                          if (p & 16)
                              x = !x
                      }
                      if (q && I % 2) {
                          if (q & 3)
                              o = !o;
                          if (q & 12)
                              s = !s;
                          if (q & 16)
                              x = !x
                      }
                      a.f = a.f || a.a & 4;
                      a.l = a.l || a.a & 8;
                      a.g = a.g || a.a & 1;
                      a.m = a.m || a.a & 2;
                      var E = s ? a.l : a.f
                        , B = s ? a.f : a.l
                        , D = o ? a.m : a.g
                        , C = o ? a.g : a.m;
                      a.a = E || B || D || C;
                      r = {};
                      e = {
                          v: 0,
                          u: 0,
                          I: 1,
                          A: n,
                          q: l
                      };
                      f = b.J({}, e);
                      t = b.J({}, g[v]);
                      if (a.I)
                          e.I = 2 - a.I;
                      if (a.T) {
                          e.T = a.T;
                          f.T = 0
                      }
                      var K = a.k * a.o > 1 || a.a;
                      if (a.V || a.C) {
                          var J = d;
                          if (J) {
                              e.V = a.V ? a.V - 1 : 1;
                              f.V = 1;
                              var N = a.C || 0;
                              e.C = N * 360 * (x ? -1 : 1);
                              f.C = 0
                          }
                      }
                      if (K) {
                          var i = t.Tb = {};
                          if (a.a) {
                              var w = a.Uf || 1;
                              if (E && B) {
                                  i.f = g.q / 2 * w;
                                  i.l = -i.f
                              } else if (E)
                                  i.l = -g.q * w;
                              else if (B)
                                  i.f = g.q * w;
                              if (D && C) {
                                  i.g = g.A / 2 * w;
                                  i.m = -i.g
                              } else if (D)
                                  i.m = -g.A * w;
                              else if (C)
                                  i.g = g.A * w
                          }
                          r.a = t;
                          f.a = g[v]
                      }
                      var L = o ? 1 : -1
                        , M = s ? 1 : -1;
                      if (a.x)
                          e.u += n * a.x * L;
                      if (a.y)
                          e.v += l * a.y * M;
                      b.i(e, function (a, c) {
                          if (b.Nc(a))
                              if (a != f[c])
                                  r[c] = a - f[c]
                      });
                      u[v] = j ? f : e;
                      var F = a.re
                        , A = c.round(m * a.L / a.cb);
                      h[v] = new Array(A);
                      h[v].ne = A;
                      h[v].je = A + F - 1;
                      for (var z = 0; z <= F; z++) {
                          var y = b.Pc(f, r, z / F, a.p, a.Dc, a.yb, {
                              vb: a.vb,
                              fb: n,
                              kb: l
                          });
                          y.T = y.T || 1;
                          h[v].push(y)
                      }
                  })
              });
              o.reverse();
              b.i(o, function (a) {
                  b.i(a, function (c) {
                      var f = c[0]
                        , e = c[1]
                        , d = f + "," + e
                        , a = i;
                      if (e || f)
                          a = b.ib(i);
                      b.B(a, u[d]);
                      b.cc(a, "hidden");
                      b.K(a, "absolute");
                      z.ge(a);
                      m[d] = a;
                      b.H(a, !j)
                  })
              })
          }
          function w() {
              var a = this
                , b = 0;
              l.call(a, 0, v);
              a.Lb = function (c, a) {
                  if (a - b > i) {
                      b = a;
                      g && g.Mb(a);
                      h && h.Mb(a)
                  }
              }
              ;
              a.pd = r
          }
          a.ke = function () {
              var a = 0
                , b = u.yc
                , d = b.length;
              if (x)
                  a = y++ % d;
              else
                  a = c.floor(c.random() * d);
              b[a] && (b[a].jb = a);
              return b[a]
          }
          ;
          a.ve = function (x, y, k, l, b, t) {
              a.mb();
              r = b;
              b = j(b, i, t);
              var f = l.dd
                , e = k.dd;
              f["no-image"] = !l.dc;
              e["no-image"] = !k.dc;
              var o = f
                , p = e
                , w = b
                , d = b.kc || j({}, i, t);
              if (!b.Z) {
                  o = e;
                  p = f
              }
              var u = d.Ob || 0;
              h = new n(m, p, d, c.max(u - d.cb, 0), s, q);
              g = new n(m, o, w, c.max(d.cb - u, 0), s, q);
              h.Mb(0);
              g.Mb(0);
              v = c.max(h.Dd, g.Dd);
              a.jb = x
          }
          ;
          a.mb = function () {
              m.mb();
              h = f;
              g = f
          }
          ;
          a.Ud = function () {
              var a = f;
              if (g)
                  a = new w;
              return a
          }
          ;
          if (z && b.rf() < 537)
              i = 16;
          o.call(a);
          l.call(a, -1e7, 1e7)
      }
      , q = {
          lc: 1
      }
      , s = function (a, E) {
          var g = this;
          o.call(g);
          a = b.Gb(a);
          var u, C, B, t, l = 0, e, p, j, y, z, i, h, s, r, D = [], A = [];
          function x(a) {
              a != -1 && A[a].Fe(a == l)
          }
          function v(a) {
              g.j(q.lc, a * p)
          }
          g.gb = a;
          g.rc = function (a) {
              if (a != t) {
                  var d = l
                    , b = c.floor(a / p);
                  l = b;
                  t = a;
                  x(d);
                  x(b)
              }
          }
          ;
          g.uc = function (c) {
              b.H(a, c)
          }
          ;
          var w;
          g.vc = function (x) {
              if (!w) {
                  u = c.ceil(x / p);
                  l = 0;
                  var n = s + y
                    , o = r + z
                    , m = c.ceil(u / j) - 1;
                  C = s + n * (!i ? m : j - 1);
                  B = r + o * (i ? m : j - 1);
                  b.s(a, C);
                  b.z(a, B);
                  for (var g = 0; g < u; g++) {
                      var t = b.wf();
                      b.vf(t, g + 1);
                      var k = b.Kf(h, "numbertemplate", t, d);
                      b.K(k, "absolute");
                      var q = g % (m + 1);
                      b.O(k, !i ? n * q : g % j * n);
                      b.U(k, i ? o * q : c.floor(g / (m + 1)) * o);
                      b.N(a, k);
                      D[g] = k;
                      e.wc & 1 && b.c(k, "click", b.M(f, v, g));
                      e.wc & 2 && b.c(k, "mouseenter", b.M(f, v, g));
                      A[g] = b.zc(k)
                  }
                  w = d
              }
          }
          ;
          g.Wb = e = b.J({
              Mc: 10,
              Wc: 10,
              Qc: 1,
              wc: 1
          }, E);
          h = b.pc(a, "prototype");
          s = b.s(h);
          r = b.z(h);
          b.ec(h, a);
          p = e.Xc || 1;
          j = e.o || 1;
          y = e.Mc;
          z = e.Wc;
          i = e.Qc - 1;
          e.Eb == k && n.sb(a, m.Xb, 1);
          e.eb && n.sb(a, m.Fb, e.eb);
          n.Kc(a)
      }
      , u = function (a, e, i, y, x, w) {
          var c = this;
          o.call(c);
          var j, h, g, l;
          b.s(a);
          b.z(a);
          var s, r;
          function p(a) {
              c.j(q.lc, a, d)
          }
          function v(c) {
              b.H(a, c);
              b.H(e, c)
          }
          function u() {
              s.bc(i.Pb || !j.uf(h));
              r.bc(i.Pb || !j.Ve(h))
          }
          c.rc = function (c, a, b) {
              h = a;
              !b && u()
          }
          ;
          c.uc = v;
          var t;
          c.vc = function () {
              h = 0;
              if (!t) {
                  b.c(a, "click", b.M(f, p, -l));
                  b.c(e, "click", b.M(f, p, l));
                  s = b.zc(a);
                  r = b.zc(e);
                  t = d
              }
          }
          ;
          c.Wb = g = b.J({
              Xc: 1
          }, i);
          l = g.Xc;
          j = w;
          if (g.Eb == k) {
              n.sb(a, m.Xb, 1);
              n.sb(e, m.Xb, 1)
          }
          if (g.eb) {
              n.sb(a, m.Fb, g.eb);
              n.sb(e, m.Fb, g.eb)
          }
          n.Kc(a);
          n.Kc(e)
      };
    function r(e, d, c) {
        var a = this;
        l.call(a, 0, c);
        a.Pd = b.Uc;
        a.Oc = 0;
        a.Lc = c
    }
    var h = function () {
        var a = this;
        b.sf(a, o);
        var Mb = "data-jssor-slider", Zb = "data-jssor-thumb", v, n, U, nb, bb, xb, ab, T, L, K, Cb, Tb = 1, lc = 1, bc = 1, cc = {}, x, V, Eb, Ib, Hb, Ab, zb, yb, jb, s = -1, Pb, p, I, M, sb, ub, vb, gb, G, S, Y, y, W, tb, eb = [], hc, jc, dc, Ub, Fc, u, kb, H, fc, rb, Nb, gc, cb, O = 0, J = Number.MAX_VALUE, E = Number.MIN_VALUE, ic, C, lb, Q, N = 1, Z, A, db, Qb = 0, Rb = 0, P, ob, pb, mb, w, hb, z, Gb, fb = [], Fb = b.mf(), qb = Fb.lf, B = [], D, R, F, Lb, Yb, X;
        function uc(e, k, o) {
            var l = this, h = {
                f: 2,
                m: 1,
                l: 2,
                g: 1
            }, n = {
                f: "top",
                m: "right",
                l: "bottom",
                g: "left"
            }, g, a, f, i, j = {};
            l.gb = e;
            l.Zb = function (q, p, t) {
                var l, s = q, r = p;
                if (!f) {
                    f = b.If(e);
                    g = e.parentNode;
                    i = {
                        Eb: b.Ub(e, m.Xb, 1),
                        eb: b.Ub(e, m.Fb)
                    };
                    b.i(n, function (c, a) {
                        j[a] = b.Ub(e, "data-scale-" + c, 1)
                    });
                    a = e;
                    if (k) {
                        a = b.ib(g, d);
                        b.B(a, {
                            f: 0,
                            g: 0
                        });
                        b.N(a, e);
                        b.N(g, a)
                    }
                }
                if (o) {
                    l = c.max(q, p);
                    if (k)
                        if (t > 0 && t < 1) {
                            var v = c.min(q, p);
                            l = c.min(l / v, 1 / (1 - t)) * v
                        }
                } else
                    s = r = l = c.pow(L < K ? p : q, i.Eb);
                b.Pe(a, l);
                b.G(a, m.Gc, l);
                b.s(g, f.A * s);
                b.z(g, f.q * r);
                var u = b.Tc() && b.Qd() < 9 || b.Qd() < 10 && b.nf() ? l : 1
                  , w = (s - u) * f.A / 2
                  , x = (r - u) * f.q / 2;
                b.O(a, w);
                b.U(a, x);
                b.i(f, function (d, a) {
                    if (h[a] && d) {
                        var e = (h[a] & 1) * c.pow(q, j[a]) * d + (h[a] & 2) * c.pow(p, j[a]) * d / 2;
                        b.Yd[a](g, e)
                    }
                });
                b.fd(g, i.eb)
            }
        }
        function Ec() {
            var b = this;
            l.call(b, -1e8, 2e8);
            b.kf = function () {
                var a = b.nb();
                a = t(a);
                var d = c.round(a)
                  , g = d
                  , f = a - c.floor(a)
                  , e = ac(a);
                return {
                    jb: g,
                    jf: d,
                    ub: f,
                    gc: a,
                    hf: e
                }
            }
            ;
            b.Lb = function (e, b) {
                var g = t(b);
                if (c.abs(b - e) > 1e-5) {
                    var f = c.floor(b);
                    if (f != b && b > e && (C & 1 || b > O))
                        f++;
                    kc(f, g, d)
                }
                a.j(h.gf, g, t(e), b, e)
            }
        }
        function Dc() {
            var a = this;
            l.call(a, 0, 0, {
                Bc: p
            });
            b.i(B, function (b) {
                C & 1 && b.le(p);
                a.Ic(b);
                b.Ob(cb / gb)
            })
        }
        function Cc() {
            var a = this
              , b = Gb.gb;
            l.call(a, -1, 2, {
                p: e.zb,
                te: {
                    ub: pc
                },
                Bc: p
            }, b, {
                ub: 1
            }, {
                ub: -2
            });
            a.nc = b
        }
        function vc(o, m) {
            var b = this, e, g, i, j, c;
            l.call(b, -1e8, 2e8, {
                vd: 100
            });
            b.ud = function () {
                Z = d;
                db = f;
                a.j(h.ff, t(w.bb()), w.bb())
            }
            ;
            b.td = function () {
                Z = k;
                j = k;
                var b = w.kf();
                a.j(h.ef, t(w.bb()), w.bb());
                if (!A) {
                    Gc(b.jf, s);
                    (!b.ub || b.hf) && kc(s, b.gc)
                }
            }
            ;
            b.Lb = function (f, d) {
                var a;
                if (j)
                    a = c;
                else {
                    a = g;
                    if (i) {
                        var b = d / i;
                        a = n.hd(b) * (g - e) + e
                    }
                }
                w.S(a)
            }
            ;
            b.Fc = function (a, d, c, f) {
                e = a;
                g = d;
                i = c;
                w.S(a);
                b.S(0);
                b.Hd(c, f)
            }
            ;
            b.df = function (a) {
                j = d;
                c = a;
                b.sd(a, f, d)
            }
            ;
            b.cf = function (a) {
                c = a
            }
            ;
            w = new Ec;
            w.Ld(o);
            w.Ld(m)
        }
        function wc() {
            var c = this
              , a = nc();
            b.F(a, 0);
            b.rb(a, "pointerEvents", "none");
            c.gb = a;
            c.ge = function (c) {
                b.N(a, c);
                b.H(a)
            }
            ;
            c.mb = function () {
                b.P(a);
                b.fc(a)
            }
        }
        function Bc(m, g) {
            var e = this, r, J, v, j, y = [], x, A, S, E, P, L, F, i, w, q;
            l.call(e, -G, G + 1, {});
            function K(a) {
                r && r.Pd();
                R(m, a, 0);
                L = d;
                r = new bb.R(m, bb, b.Yc(b.n(m, "idle")) || fc, !u);
                r.S(0)
            }
            function W() {
                r.Cc < bb.Cc && K()
            }
            function N(p, r, o) {
                if (!E) {
                    E = d;
                    if (j && o) {
                        var f = o.width
                          , c = o.height
                          , m = f
                          , l = c;
                        if (f && c && n.Jb) {
                            if (n.Jb & 3 && (!(n.Jb & 4) || f > I || c > M)) {
                                var i = k
                                  , q = I / M * c / f;
                                if (n.Jb & 1)
                                    i = q > 1;
                                else if (n.Jb & 2)
                                    i = q < 1;
                                m = i ? f * M / c : I;
                                l = i ? M : c * I / f
                            }
                            b.s(j, m);
                            b.z(j, l);
                            b.U(j, (M - l) / 2);
                            b.O(j, (I - m) / 2)
                        }
                        b.K(j, "absolute");
                        a.j(h.bf, g)
                    }
                }
                b.P(r);
                p && p(e)
            }
            function U(f, b, c, d) {
                if (d == db && s == g && u)
                    if (!Fc) {
                        var a = t(f);
                        D.ve(a, g, b, e, c, M / I);
                        b.Ee();
                        hb.Ob(a - hb.qd() - 1);
                        hb.S(a);
                        z.Fc(a, a, 0)
                    }
            }
            function Y(b) {
                if (b == db && s == g) {
                    if (!i) {
                        var a = f;
                        if (D)
                            if (D.jb == g)
                                a = D.Ud();
                            else
                                D.mb();
                        W();
                        i = new Ac(m, g, a, r);
                        i.od(q)
                    }
                    !i.Od() && i.Jc()
                }
            }
            function C(a, d, k) {
                if (a == g) {
                    if (a != d)
                        B[d] && B[d].Fd();
                    else
                        !k && i && i.tf();
                    q && q.bc();
                    var l = db = b.hb();
                    e.Hb(b.M(f, Y, l))
                } else {
                    var j = c.min(g, a)
                      , h = c.max(g, a)
                      , o = c.min(h - j, j + p - h)
                      , m = G + n.Ze - 1;
                    (!P || o <= m) && e.Hb()
                }
            }
            function Z() {
                if (s == g && i) {
                    i.lb();
                    q && q.Xe();
                    q && q.Ge();
                    i.yd()
                }
            }
            function ab() {
                s == g && i && i.lb()
            }
            function X(b) {
                !Q && a.j(h.He, g, b)
            }
            function O() {
                q = w.pInstance;
                i && i.od(q)
            }
            e.Hb = function (e, c) {
                c = c || v;
                if (y.length && !E) {
                    b.H(c);
                    if (!S) {
                        S = d;
                        a.j(h.Ie, g);
                        b.i(y, function (a) {
                            if (!b.G(a, "src")) {
                                a.src = b.n(a, "src2") || "";
                                b.tb(a, a["display-origin"])
                            }
                        })
                    }
                    b.Jf(y, j, b.M(f, N, e, c))
                } else
                    N(e, c)
            }
            ;
            e.Je = function () {
                if (p == 1) {
                    e.Fd();
                    C(g, g)
                } else if (D) {
                    var a = D.ke(p);
                    if (a) {
                        var h = db = b.hb()
                          , c = g + kb
                          , d = B[t(c)];
                        return d.Hb(b.M(f, U, c, d, a, h), v)
                    }
                } else
                    Bb(kb)
            }
            ;
            e.hc = function () {
                C(g, g, d)
            }
            ;
            e.Fd = function () {
                q && q.Xe();
                q && q.Ge();
                e.Ad();
                i && i.Ye();
                i = f;
                K()
            }
            ;
            e.Ee = function () {
                b.P(m)
            }
            ;
            e.Ad = function () {
                b.H(m)
            }
            ;
            e.Pf = function () {
                q && q.bc()
            }
            ;
            function R(a, f, c, h) {
                if (b.G(a, Mb))
                    return;
                if (!L) {
                    if (a.tagName == "IMG") {
                        y.push(a);
                        if (!b.G(a, "src")) {
                            P = d;
                            a["display-origin"] = b.tb(a);
                            b.P(a)
                        }
                    }
                    var e = b.Xd(a);
                    if (e) {
                        var g = new Image;
                        b.n(g, "src2", e);
                        y.push(g)
                    }
                    c && b.F(a, (b.F(a) || 0) + 1)
                }
                var i = b.Nb(a);
                b.i(i, function (a) {
                    var e = a.tagName
                      , g = b.n(a, "u");
                    if (g == "player" && !w) {
                        w = a;
                        if (w.pInstance)
                            O();
                        else
                            b.c(w, "dataavailable", O)
                    }
                    if (g == "caption") {
                        if (f) {
                            b.Ke(a, b.n(a, "to"));
                            b.Le(a, b.n(a, "bf"));
                            F && b.n(a, "3d") && b.Me(a, "preserve-3d")
                        }
                    } else if (!L && !c && !j) {
                        if (e == "A") {
                            if (b.n(a, "u") == "image")
                                j = b.Ue(a, "IMG");
                            else
                                j = b.pc(a, "image", d);
                            if (j) {
                                x = a;
                                b.tb(x, "block");
                                b.B(x, jb);
                                A = b.ib(x, d);
                                b.K(x, "relative");
                                b.jc(A, 0);
                                b.rb(A, "backgroundColor", "#000")
                            }
                        } else if (e == "IMG" && b.n(a, "u") == "image")
                            j = a;
                        if (j) {
                            j.border = 0;
                            b.B(j, jb)
                        }
                    }
                    R(a, f, c + 1, h)
                })
            }
            e.mc = function (c, b) {
                var a = G - b;
                pc(J, a)
            }
            ;
            e.jb = g;
            o.call(e);
            F = b.n(m, "p");
            b.Ne(m, F);
            b.Oe(m, b.n(m, "po"));
            var H = b.pc(m, "thumb", d);
            if (H) {
                b.ib(H);
                b.P(H)
            }
            b.H(m);
            v = b.ib(V);
            b.F(v, 1e3);
            b.c(m, "click", X);
            K(d);
            e.dc = j;
            e.gd = A;
            e.dd = m;
            e.nc = J = m;
            b.N(J, v);
            a.Bb(203, C);
            a.Bb(28, ab);
            a.Bb(24, Z)
        }
        function Ac(z, g, p, q) {
            var c = this, n = 0, v = 0, i, j, f, e, m, t, r, o = B[g];
            l.call(c, 0, 0);
            function w() {
                b.fc(R);
                Ub && m && o.gd && b.N(R, o.gd);
                b.H(R, !m && o.dc)
            }
            function x() {
                c.Jc()
            }
            function y(a) {
                r = a;
                c.lb();
                c.Jc()
            }
            c.Jc = function () {
                var b = c.nb();
                if (!A && !Z && !r && s == g) {
                    if (!b) {
                        if (i && !m) {
                            m = d;
                            c.yd(d);
                            a.j(h.Qf, g, n, v, i, e)
                        }
                        w()
                    }
                    var k, p = h.ld;
                    if (b != e)
                        if (b == f)
                            k = e;
                        else if (b == j)
                            k = f;
                        else if (!b)
                            k = j;
                        else
                            k = c.Md();
                    a.j(p, g, b, n, j, f, e);
                    var l = u && (!H || N);
                    if (b == e)
                        (f != e && !(H & 12) || l) && o.Je();
                    else 
                        (l || b != f) && c.Hd(k, x)
                }
            }
            ;
            c.tf = function () {
                f == e && f == c.nb() && c.S(j)
            }
            ;
            c.Ye = function () {
                D && D.jb == g && D.mb();
                var b = c.nb();
                b < e && a.j(h.ld, g, -b - 1, n, j, f, e)
            }
            ;
            c.yd = function (a) {
                p && b.cc(Y, a && p.pd.xf ? "" : "hidden")
            }
            ;
            c.mc = function (c, b) {
                if (m && b >= i) {
                    m = k;
                    w();
                    o.Ad();
                    D.mb();
                    a.j(h.yf, g, n, v, i, e)
                }
                a.j(h.zf, g, b, n, j, f, e)
            }
            ;
            c.od = function (a) {
                if (a && !t) {
                    t = a;
                    a.Bb($JssorPlayer$.Ae, y)
                }
            }
            ;
            p && c.Ic(p);
            i = c.ic();
            c.Ic(q);
            j = i + q.Oc;
            e = c.ic();
            f = u ? i + q.Lc : e
        }
        function Ob(a, c, d) {
            b.O(a, c);
            b.U(a, d)
        }
        function pc(c, b) {
            var a = y > 0 ? y : U
              , d = ub * b * (a & 1)
              , e = vb * b * (a >> 1 & 1);
            Ob(c, d, e)
        }
        function Kb(a) {
            if (!(C & 1))
                a = c.min(J, c.max(a, E));
            return a
        }
        function ac(a) {
            return !(C & 1) && (a - E < .0001 || J - a < .0001)
        }
        function ec() {
            Lb = Z;
            Yb = z.Md();
            F = w.bb()
        }
        function Wb() {
            ec();
            if (A || !N && H & 12) {
                z.lb();
                a.j(h.Af)
            }
        }
        function Vb(g) {
            if (!A && (N || !(H & 12)) && !z.Od()) {
                var b = w.bb()
                  , a = F
                  , f = 0;
                if (g && c.abs(P) >= n.Ed) {
                    a = b;
                    f = pb
                }
                if (ac(b)) {
                    if (!g || Q)
                        a = c.round(a)
                } else
                    a = c.ceil(a);
                a = Kb(a + f);
                if (!(C & 1)) {
                    if (J - a < .5)
                        a = J;
                    if (a - E < .5)
                        a = E
                }
                var d = c.abs(a - b);
                if (d < 1 && n.hd != e.zb)
                    d = 1 - c.pow(1 - d, 5);
                if (!Q && Lb)
                    z.ae(Yb);
                else if (b == a) {
                    Pb.Pf();
                    Pb.hc()
                } else
                    z.Fc(b, a, d * rb)
            }
        }
        function Xb(a) {
            !b.Ab(b.Ec(a), "nodrag") && b.Qb(a)
        }
        function yc(a) {
            oc(a, 1)
        }
        function oc(c, g) {
            var e = b.Ec(c);
            tb = k;
            var l = b.Ab(e, "1", Zb);
            if ((!l || l === v) && !W && (!g || c.touches.length == 1)) {
                tb = b.Ab(e, "nodrag") || !lb || !zc();
                var n = b.Ab(e, i, m.Gc);
                if (n)
                    bc = b.G(n, m.Gc);
                if (g) {
                    var p = c.touches[0];
                    Qb = p.clientX;
                    Rb = p.clientY
                } else {
                    var o = b.rd(c);
                    Qb = o.x;
                    Rb = o.y
                }
                A = d;
                db = f;
                b.c(j, g ? "touchmove" : "mousemove", Db);
                b.hb();
                Q = 0;
                Wb();
                if (!Lb)
                    y = 0;
                P = 0;
                ob = 0;
                pb = 0;
                a.j(h.Bf, t(F), F, c)
            }
        }
        function Db(g) {
            if (A) {
                var a;
                if (g.type != "mousemove")
                    if (g.touches.length == 1) {
                        var o = g.touches[0];
                        a = {
                            x: o.clientX,
                            y: o.clientY
                        }
                    } else
                        ib();
                else
                    a = b.rd(g);
                if (a) {
                    var e = a.x - Qb
                      , f = a.y - Rb;
                    if (y || c.abs(e) > 1.5 || c.abs(f) > 1.5) {
                        if (c.floor(F) != F)
                            y = y || U & W;
                        if ((e || f) && !y) {
                            if (W == 3)
                                if (c.abs(f) > c.abs(e))
                                    y = 2;
                                else
                                    y = 1;
                            else
                                y = W;
                            if (qb && y == 1 && c.abs(f) > c.abs(e) * 2.4)
                                tb = d
                        }
                        var n = f
                          , i = vb;
                        if (y == 1) {
                            n = e;
                            i = ub
                        }
                        if (P - ob < -1.5)
                            pb = 0;
                        else if (P - ob > 1.5)
                            pb = -1;
                        ob = P;
                        P = n;
                        X = F - P / i / bc;
                        if (!(C & 1)) {
                            var l = 0
                              , j = [-F + O, 0, F - p + S - O];
                            b.i(j, function (b, d) {
                                if (b > 0) {
                                    var a = c.pow(b, 1 / 1.6);
                                    a = c.tan(a * c.PI / 2);
                                    l = (a - b) * (d - 1)
                                }
                            });
                            var h = l + X
                              , m = k;
                            j = [-h + O, 0, h - p + S - O];
                            b.i(j, function (a, b) {
                                if (a > 0) {
                                    a = c.min(a, i);
                                    a = c.atan(a) * 2 / c.PI;
                                    a = c.pow(a, 1.6);
                                    X = a * (b - 1) + O;
                                    if (b)
                                        X += p - S;
                                    m = d
                                }
                            });
                            if (!m)
                                X = h
                        }
                        if (P && y && !tb) {
                            b.Qb(g);
                            if (!Z)
                                z.df(X);
                            else
                                z.cf(X)
                        }
                    }
                }
            }
        }
        function ib() {
            xc();
            if (A) {
                Q = P;
                b.hb();
                b.Q(j, "mousemove", Db);
                b.Q(j, "touchmove", Db);
                Q && u & 8 && (u = 0);
                z.lb();
                A = k;
                var c = w.bb();
                a.j(h.Cf, t(c), c, t(F), F);
                H & 12 && ec();
                Vb(d)
            }
        }
        function tc(c) {
            var a = b.Ec(c)
              , d = b.Ab(a, "1", Mb);
            if (v === d)
                if (Q) {
                    b.Se(c);
                    while (a && v !== a) {
                        (a.tagName == "A" || b.G(a, "data-jssor-button")) && b.Qb(c);
                        a = a.parentNode
                    }
                } else
                    u & 4 && (u = 0)
        }
        function Hc(d) {
            if (d != s) {
                var b = mb.nb()
                  , a = Kb(d)
                  , e = c.round(t(a));
                if (b - a < .5)
                    a = b;
                B[s];
                s = e;
                Pb = B[s];
                w.S(a)
            }
        }
        function Gc(b, c) {
            y = 0;
            Hc(b);
            if (u & 2 && (kb > 0 && s == p - 1 || kb < 0 && !s))
                u = 0;
            a.j(h.Ef, s, c)
        }
        function kc(a, d, e) {
            if (!(C & 1)) {
                a = c.max(0, a);
                a = c.min(a, p - S + O);
                a = c.round(a)
            }
            a = t(a);
            b.i(eb, function (b) {
                b.rc(a, d, e)
            })
        }
        function zc() {
            var b = h.ed || 0
              , a = lb;
            h.ed |= a;
            return W = a & ~b
        }
        function xc() {
            if (W) {
                h.ed &= ~lb;
                W = 0
            }
        }
        function nc() {
            var a = b.oc();
            b.B(a, jb);
            b.K(a, "absolute");
            return a
        }
        function t(b, a) {
            a = a || p || 1;
            return (b % a + a) % a
        }
        function Jb(c, a, b) {
            u & 8 && (u = 0);
            wb(c, rb, a, b)
        }
        function Sb() {
            b.i(eb, function (a) {
                a.uc(a.Wb.Xf <= N)
            })
        }
        function rc() {
            if (!N) {
                N = 1;
                Sb();
                if (!A) {
                    H & 12 && Vb();
                    H & 3 && B[s] && B[s].hc()
                }
            }
            a.j(h.Gf)
        }
        function qc() {
            if (N) {
                N = 0;
                Sb();
                A || !(H & 12) || Wb()
            }
            a.j(h.Hf)
        }
        function sc() {
            b.i(fb, function (a) {
                b.B(a, jb);
                b.K(a, "absolute");
                b.cc(a, "hidden");
                b.P(a)
            });
            b.B(V, jb)
        }
        function Bb(b, a) {
            wb(b, a, d)
        }
        function wb(l, f, m, o) {
            if (!A && (N || !(H & 12)) || n.Cd) {
                Z = d;
                A = k;
                z.lb();
                if (f == i)
                    f = rb;
                var b = t(mb.nb())
                  , e = l;
                if (m) {
                    e = b + l;
                    e = c.round(e)
                }
                var a = e;
                if (!(C & 1)) {
                    if (o)
                        a = t(a);
                    else if (C & 2 && (a < 0 && c.abs(b - E) < .0001 || a > p - S && c.abs(b - J) < .0001))
                        a = a < 0 ? J : E;
                    a = Kb(a);
                    if (J - a < .5)
                        a = J;
                    if (a - E < .5)
                        a = E
                }
                var j = (a - b) % p;
                a = b + j;
                var g = b == a ? 0 : f * c.abs(j)
                  , h = 1;
                if (G > 1)
                    h = (U & 1 ? zb : yb) / gb;
                g = c.min(g, f * h * 1.5);
                z.Fc(b, a, g || 1)
            }
        }
        a.wb = function (a) {
            if (a == i)
                return u;
            if (a != u) {
                u = a;
                u && B[s] && B[s].hc()
            }
        }
        ;
        a.fb = function () {
            return L
        }
        ;
        a.kb = function () {
            return K
        }
        ;
        a.ee = function (b) {
            if (b == i)
                return Cb || L;
            a.Zb(b, b / L * K)
        }
        ;
        a.Zb = function (c, a, d) {
            b.s(v, c);
            b.z(v, a);
            Tb = c / L;
            lc = a / K;
            b.i(cc, function (a) {
                a.Zb(Tb, lc, d)
            });
            if (!Cb) {
                b.Cb(Y, x);
                b.U(Y, 0);
                b.O(Y, 0)
            }
            Cb = c
        }
        ;
        a.uf = function (a) {
            return c.abs(a - E) < .0001
        }
        ;
        a.Ve = function (a) {
            return c.abs(a - J) < .0001
        }
        ;
        a.sd = function () {
            a.wb(u || 1)
        }
        ;
        a.Sb = function (s, l) {
            a.gb = v = b.Gb(s);
            L = b.s(v);
            K = b.z(v);
            n = b.J({
                Jb: 0,
                Ze: 1,
                Db: 1,
                qc: 0,
                wb: 0,
                Pb: 1,
                xc: d,
                Cd: d,
                he: 1,
                Bd: 3e3,
                zd: 1,
                xd: 500,
                hd: e.Yb,
                Ed: 20,
                Sd: 0,
                k: 1,
                Hc: 0,
                De: 1,
                Jd: 1,
                Nd: 1
            }, l);
            n.xc = n.xc && b.qf();
            if (n.pe != i)
                n.Bd = n.pe;
            if (n.we != i)
                n.Hc = n.we;
            U = n.Jd & 3;
            nb = n.ue;
            bb = b.J({
                R: r
            }, n.Tf);
            xb = n.Ce;
            ab = n.ce;
            T = n.Vf;
            !n.De;
            var w = b.Nb(v);
            b.i(w, function (a, d) {
                var c = b.n(a, "u");
                if (c == "loading")
                    V = a;
                else {
                    if (c == "slides")
                        x = a;
                    if (c == "navigator")
                        Eb = a;
                    if (c == "arrowleft")
                        Ib = a;
                    if (c == "arrowright")
                        Hb = a;
                    if (c == "thumbnavigator")
                        Ab = a;
                    if (a.tagName != "STYLE" && a.tagName != "SCRIPT")
                        cc[c || d] = new uc(a, c == "slides", b.Sf(["slides", "thumbnavigator"])[c])
                }
            });
            V = V || b.oc(j);
            zb = b.s(x);
            yb = b.z(x);
            I = n.Re || zb;
            M = n.Qe || yb;
            jb = {
                A: I,
                q: M,
                f: 0,
                g: 0
            };
            sb = n.Sd;
            ub = I + sb;
            vb = M + sb;
            gb = U & 1 ? ub : vb;
            kb = n.he;
            H = n.zd;
            fc = n.Bd;
            rb = n.xd;
            Gb = new wc;
            if (n.xc && (!b.of() || qb))
                Ob = function (a, c, d) {
                    b.ac(a, {
                        pb: c,
                        ob: d
                    })
                }
            ;
            u = n.wb & 63;
            a.Wb = l;
            b.G(v, Mb, "1");
            b.F(x, b.F(x) || 0);
            b.K(x, "absolute");
            Y = b.ib(x, d);
            b.Cb(Y, x);
            hb = new Cc;
            b.N(Y, hb.nc);
            b.cc(x, "hidden");
            H &= qb ? 10 : 5;
            var y = b.Nb(x);
            b.i(y, function (a) {
                a.tagName == "DIV" && !b.n(a, "u") && fb.push(a);
                b.F(a, (b.F(a) || 0) + 1)
            });
            R = nc();
            b.rb(R, "backgroundColor", "#000");
            b.jc(R, 0);
            b.F(R, 0);
            b.Cb(R, x.firstChild, x);
            p = fb.length;
            G = c.min(n.k, p);
            S = ((U & 1) * zb + (U & 2) * yb / 2 + sb) / gb;
            ic = G < p;
            C = ic ? n.Pb : 0;
            if (p) {
                sc();
                cb = G >= p ? 0 : n.Hc;
                if (nb) {
                    Ub = nb.Yf;
                    Nb = nb.R;
                    gc = !cb && G == 1 && p > 1 && Nb && (!b.Tc() || b.Id() >= 9)
                }
                if (!(C & 1)) {
                    O = cb / gb;
                    E = O;
                    J = E + p - S
                }
                lb = (G > 1 || cb ? U : -1) & n.Nd;
                Fb.Rc && b.rb(x, Fb.Rc, ([f, "pan-y", "pan-x", "none"])[lb] || "");
                if (gc)
                    D = new Nb(Gb, I, M, nb, qb, Ob);
                for (var h = 0; h < fb.length; h++) {
                    var m = fb[h]
                      , o = new Bc(m, h);
                    B.push(o)
                }
                b.P(V);
                mb = new Dc;
                z = new vc(mb, hb);
                b.c(v, "click", tc, d);
                b.c(v, "mouseleave", rc);
                b.c(v, "mouseenter", qc);
                b.c(v, "mousedown", oc);
                b.c(v, "touchstart", yc);
                b.c(v, "dragstart", Xb);
                b.c(v, "selectstart", Xb);
                b.c(g, "mouseup", ib);
                b.c(j, "mouseup", ib);
                b.c(j, "touchend", ib);
                b.c(j, "touchcancel", ib);
                b.c(g, "blur", ib);
                if (Eb && xb) {
                    hc = new xb.R(Eb, xb, L, K);
                    eb.push(hc)
                }
                if (ab && Ib && Hb) {
                    ab.Pb = C;
                    jc = new ab.R(Ib, Hb, ab, L, K, a);
                    eb.push(jc)
                }
                if (Ab && T) {
                    T.qc = n.qc;
                    T.Db = T.Db || 0;
                    dc = new T.R(Ab, T);
                    !T.Wf && b.G(Ab, Zb, "1");
                    eb.push(dc)
                }
                b.i(eb, function (a) {
                    a.vc(p, B, V);
                    a.Bb(q.lc, Jb)
                });
                b.rb(v, "visibility", "visible");
                a.Zb(L, K);
                Sb();
                n.Db && b.c(j, "keydown", function (a) {
                    if (a.keyCode == 37)
                        Jb(-n.Db, d);
                    else
                        a.keyCode == 39 && Jb(n.Db, d)
                });
                var k = n.qc;
                k = t(k);
                wb(k, 0)
            }
        }
        ;
        b.Sb(a)
    };
    h.He = 21;
    h.Bf = 22;
    h.Cf = 23;
    h.ff = 24;
    h.ef = 25;
    h.Ie = 26;
    h.bf = 27;
    h.Af = 28;
    h.Hf = 31;
    h.Gf = 32;
    h.gf = 202;
    h.Ef = 203;
    h.Qf = 206;
    h.yf = 207;
    h.zf = 208;
    h.ld = 209;
    jssor_1_slider_init = function () {
        var i = [{
            Y: 500,
            L: 30,
            k: 8,
            o: 4,
            a: 15,
            Z: d,
            xb: p.Sc,
            Ib: 2049,
            p: e.Yb
        }, {
            Y: 500,
            L: 80,
            k: 8,
            o: 4,
            a: 15,
            Z: d,
            p: e.Yb
        }, {
            Y: 1e3,
            x: -.2,
            L: 40,
            k: 12,
            Z: d,
            xb: p.cd,
            Ib: 260,
            p: {
                g: e.Ff,
                I: e.Be
            },
            I: 2,
            xf: d,
            yb: {
                f: .5
            }
        }, {
            Y: 2e3,
            y: -1,
            L: 60,
            k: 15,
            Z: d,
            xb: p.cd,
            p: e.Te,
            yb: {
                f: 1.5
            }
        }, {
            Y: 1200,
            x: .2,
            y: -.1,
            L: 20,
            k: 8,
            o: 4,
            a: 15,
            Dc: {
                g: [.3, .7],
                f: [.3, .7]
            },
            xb: p.Sc,
            Ib: 260,
            p: {
                g: e.wd,
                f: e.wd,
                a: e.Yb
            },
            yb: {
                g: 1.3,
                f: 2.5
            }
        }]
          , j = {
              wb: 1,
              ue: {
                  R: t,
                  yc: i,
                  xe: 1
              },
              ce: {
                  R: u
              },
              Ce: {
                  R: s
              }
          }
          , f = new h("jssor_1", j)
          , k = 1110;
        function a() {
            var d = f.gb.parentNode
              , b = d.clientWidth;
            if (b) {
                var e = c.min(k || b, b);
                f.ee(e)
            } else
                g.setTimeout(a, 30)
        }
        a();
        b.c(g, "load", a);
        b.c(g, "resize", a);
        b.c(g, "orientationchange", a)
    }
}
)(window, document, Math, null, true, false)
