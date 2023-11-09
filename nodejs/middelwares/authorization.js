const jwt = require("jsonwebtoken");
const SECRET_KEY = "secretkey";

const auth = (req, res, next) => {

    try {
        
        let token = req.query.token || req.body.token;
        if (token) {
            let user = jwt.verify(token, SECRET_KEY);
            // return res.json({ user: user});
            req.id = user.id;
            next();
        } else {
            res.status(401).json({ msg: "Unauthorized User from else" });
        }

        
    } catch (error) {
        console.log(error);
        res.status(401).json({ msg: "Unauthorized User from catch" });
    }
}

module.exports = auth;