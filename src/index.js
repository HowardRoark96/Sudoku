import './styles/index.scss';
import 'normalize.css';
import '../node_modules/font-awesome/scss/font-awesome.scss';

import './app.module';
import './modules';

import requireAll from './common/requireAll';

requireAll(require.context('./directives', true, /\.js$/));
requireAll(require.context('./directives', true, /\.scss$/));