import './styles/index.scss';

import './app.module';
import './modules';

import requireAll from './common/requireAll';

requireAll(require.context('./directives', true, /\.js$/));
requireAll(require.context('./directives', true, /\.scss$/));