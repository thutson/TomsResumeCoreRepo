"use strict";

// Load plugins
const autoprefixer = require("gulp-autoprefixer");
const cleanCSS = require("gulp-clean-css");
const del = require("del");
const gulp = require("gulp");
const header = require("gulp-header");
const merge = require("merge-stream");
const plumber = require("gulp-plumber");
const rename = require("gulp-rename");
const sass = require("gulp-sass");
const uglify = require("gulp-uglify");
const concat = require('gulp-concat');

// Load package.json for banner
const pkg = require('./package.json');

// Set the banner content
const banner = ['/*!\n',
    ' * <%= pkg.title %> v<%= pkg.version %> (<%= pkg.homepage %>)\n',
    ' * Copyright ' + (new Date()).getFullYear(), ' <%= pkg.author %>\n',
    ' * Licensed under <%= pkg.license %> (https://opensource.org/licenses/MIT)\n',
    ' */\n',
    '\n'
].join('');

// Clean vendor
function clean() {
    return del(["./wwwroot/lib/"]);
}

// Bring third party dependencies from node_modules into vendor directory
function modules() {
    // Bootstrap
    var bootstrap = gulp.src('./node_modules/bootstrap/dist/**/*')
        .pipe(gulp.dest('./wwwroot/lib/bootstrap'));
    // Font Awesome
    var fontAwesome = gulp.src('./node_modules/@fortawesome/**/*')
        .pipe(gulp.dest('./wwwroot/lib'));
    // jQuery Easing
    var jqueryEasing = gulp.src('./node_modules/jquery.easing/*.js')
        .pipe(gulp.dest('./wwwroot/lib/jquery-easing'));
    // jQuery
    var jquery = gulp.src([
        './node_modules/jquery/dist/*',
        '!./node_modules/jquery/dist/core.js'
    ])
        .pipe(gulp.dest('./wwwroot/lib/jquery'));
    // jQuery Validation
    var jqueryValidation = gulp.src('./node_modules/jquery-validation/dist/**/*')
        .pipe(gulp.dest('./wwwroot/lib/jquery-validation'));

    return merge(bootstrap, fontAwesome, jquery, jqueryEasing, jqueryValidation);
}

// CSS task
function css() {
    return gulp
        .src("./Sass/**/*.scss")
        .pipe(plumber())
        .pipe(sass({
            outputStyle: "expanded",
            includePaths: "./node_modules"
        }))
        .on("error", sass.logError)
        .pipe(autoprefixer({
            browsers: ['last 2 versions'],
            cascade: false
        }))
        .pipe(header(banner, {
            pkg: pkg
        }))
        .pipe(gulp.dest("./wwwroot/css"))
        .pipe(rename({
            suffix: ".min"
        }))
        .pipe(cleanCSS())
        .pipe(gulp.dest("./wwwroot/css"));
}

// JS task
function minJs() {
    return gulp
        .src([
            './Scripts/*.js'
        ])
        .pipe(uglify())
        .pipe(header(banner, {
            pkg: pkg
        }))
        .pipe(concat('site.min.js'))
        .pipe(gulp.dest('./wwwroot/js'));
}

function fullJs() {
    return gulp
        .src([
            './Scripts/*.js'
        ])
        .pipe(header(banner, {
            pkg: pkg
        }))
        .pipe(gulp.dest('./wwwroot/js'));t
}

// Watch files
function watchFiles() {
    gulp.watch("./Sass/**/*", css);
    gulp.watch("./Scripts/**/*.js", js);
}

// Define complex tasks
const vendor = gulp.series(clean, modules);
const js = gulp.parallel(minJs, fullJs);
const build = gulp.series(vendor, gulp.parallel(css, js));
const watch = gulp.series(build, watchFiles);

// Export tasks
exports.css = css;
exports.js = js;
exports.clean = clean;
exports.vendor = vendor;
exports.build = build;
exports.watch = watch;
exports.default = build;