const mongoose = require("mongoose");

const StudentSchema = mongoose.Schema(
  {
    name: String,
    roll: {
      type: Number,
      unique: true,
    },
    dob: {
      type: Date,
    },
    score: Number,
  },
  { timestapms: true }
);

module.exports = mongoose.model("Student", StudentSchema);
