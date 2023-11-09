var express = require("express");
const studrouter = express.Router();
const { search, viewresult } = require("../controllers/studentController");

studrouter.get("/search", search);
// studrouter.get("/show", )

studrouter.post("/view", viewresult);


module.exports = studrouter;