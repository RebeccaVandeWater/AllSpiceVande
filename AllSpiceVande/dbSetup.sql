CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    recipes(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        title VARCHAR(255) NOT NULL,
        instructions VARCHAR(700) NOT NULL,
        img VARCHAR(700) NOT NULL,
        category ENUM(
            'Cheese',
            'Italian',
            'Specialty Coffee',
            'Mexican',
            'Soup',
            'Beef',
            'Chicken',
            'Fish',
            'Indian',
            'Chinese',
            'Japanese',
            'Korean',
            'American',
            'Vegetarian',
            'Vegan',
            'Misc'
        ) DEFAULT 'Misc',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    ingredients(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        name VARCHAR(255) NOT NULL,
        quantity INT NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE recipes;

INSERT INTO
    recipes (
        title,
        instructions,
        category,
        creatorId
    )
VALUES (
        'Spicy Ramen',
        'For the easy recipe, just cook some instant ramen and add Gochujang to the boiling water with the seasoning packet. Then when the ramen is finished cooking, add your ramen and preferred amount of broth to a bowl. Top with chopped green onions and seaweed. Yum!',
        'Japanese',
        '64dd370a7ca482920883ae34'
    );

SELECT LAST_INSERT_ID ;

SELECT * FROM recipes;

SELECT * FROM recipes WHERE id = 1;

SELECT * FROM recipes WHERE creatorId = '64dd370a7ca482920883ae34';

UPDATE recipes
SET
    instructions = 'Butter your bread. Put on greased pan to begin cooking on a pan with bread spread evenly apart. Flip bread at desired brown-ness. Put shredded or sliced preferred cheese on newly flipped bread. Cover pan with lid. Allow cheese to melt, then put both slices together. Slice diagonally and enjoy!'
WHERE id = 1;

DELETE FROM recipes WHERE id = 1 LIMIT 1;