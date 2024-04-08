CREATE TABLE History (
    id SERIAL PRIMARY KEY,
    "Date" DATE NOT NULL,
    "Source" VARCHAR(3) NOT NULL,
    "Target" VARCHAR(3) NOT NULL,
    "Value" NUMERIC NOT NULL,
    "Result" NUMERIC NOT NULL
);
