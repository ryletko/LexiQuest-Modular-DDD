module.exports = function (grunt) {

    // Project configuration.
    grunt.initConfig({
        // Compile SCSS to CSS
        sass: {
            dist: {
                files: {
                    './scss/styles.css': './scss/styles.scss' // Compile SCSS to a temp CSS file
                }
            }
        },
        // Minify the compiled CSS
        cssmin: {
            target: {
                files: {
                    './scss/styles.min.css': ['./scss/styles.css'], // Minify and output to destination
                    './app.min.css': ['./app.css'] // Minify and output to destination
                }
            }
        },
        // Copy the minified file to the destination
        copy: {
            mincss: {
                files: [
                    // Copy to ../wwwroot/css/
                    {expand: true, cwd: './scss', src: ['styles.min.css'], dest: '../wwwroot/css/'},
                    {expand: true, cwd: './', src: ['app.min.css'], dest: '../wwwroot/css/'}
                ]
            },
            minjs: {
                files: [{
                    expand: true,              // Enable dynamic expansion
                    cwd: './js/',               // Source folder for minified files
                    src: ['**/*.min.js'],      // Only match minified JS files
                    dest: '../wwwroot/js/'        // Destination folder
                }]
            }
        },
        uglify: {
            build: {
                files: [{
                    expand: true,       // Enable dynamic expansion
                    cwd: './js/',        // Source folder (root directory of your JS files)
                    src: ['**/*.js', '!**/*.min.js'],   // Match all JS files in all subdirectories
                    dest: './js/',       // Save minified files in the same directory as the original files
                    ext: '.min.js',     // Replace the .js extension with .min.js
                    extDot: 'first'     // Replace only the first dot in the filename
                }]
            }
        }
    });

    // Load the plugins
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-uglify');

    // Default task(s)
    grunt.registerTask('css', ['sass', 'cssmin', 'copy:mincss']);
    grunt.registerTask('js', ['uglify', 'copy:minjs']);
};