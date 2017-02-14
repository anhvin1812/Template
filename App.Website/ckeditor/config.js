/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'vi';
	// config.uiColor = '#AADC6E';
	 config.toolbar =[
	 	{ name: 'document', items: [ 'Source', '-', 'NewPage', 'Preview'] },
	 	{ name: 'clipboard', items: [ 'Cut', 'Copy', 'Paste', '-', 'Undo', 'Redo' ] },
	 	{ name: 'linkitems', items: [ 'Link', 'Unlink', 'Anchor'] },
	 	{ name: 'mediaitems', items: [ 'Image', 'Table', 'HorizontalRule', 'SpecialChar', 'Iframe'] },
    	
    	{ name: 'basicstyles', items: [ 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript'] },
    	{ name: 'textalign', items: [ 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
    	{ name: 'list', items: [ 'NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote'] },
    	'/',
    	{ name: 'styles', items: [ 'Format', 'Font', 'FontSize', 'TextColor', 'BGColor'] },
    	{ name: 'maximize', items: [ 'Maximize'] }
	 ];
};
