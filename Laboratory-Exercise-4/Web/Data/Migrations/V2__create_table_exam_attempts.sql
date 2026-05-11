CREATE TABLE ExamAttempts
(
    Id TEXT PRIMARY KEY NOT NULL,

    ExamId TEXT NOT NULL,
    StudentId TEXT NOT NULL,

    StartedAt TEXT NOT NULL,
    FinishedAt TEXT NULL
);